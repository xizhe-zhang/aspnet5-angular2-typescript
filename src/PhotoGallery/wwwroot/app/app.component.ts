/// <reference path="../../typings/globals/es6-shim/index.d.ts" />
/// <reference path="../../typings/globals/jquery/index.d.ts" />
/// <reference path="../../typings/globals/signalr/index.d.ts" />
/// <reference path="variable.d.ts" />

import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import 'rxjs/add/operator/map';
import { enableProdMode } from '@angular/core';

enableProdMode();
import { MembershipService } from './core/services/membership.service';
import { User } from './core/domain/user';
import { FeedService } from './core/services/feed.service';
import { SignalRConnectionStatus } from './core/interfaces';

@Component({
    selector: 'pos-app',
    templateUrl: './app/app.component.html',
    providers: [FeedService]
})
export class AppComponent implements OnInit {

    public wechatName: string = 'test';
    public wechatImageURL: string = 'logo.png';
    public isLoginOK: boolean = false;
    public qrcode: any;
    public isLoadingOK: boolean = false;
    public isCounterDown: boolean = false;

    constructor(public membershipService: MembershipService,
        public location: Location, private feedService: FeedService) {
    }

    ngOnInit() {
        this.location.go('/');

        this.qrcode = new QRCode(document.getElementById("qrcode"), {
            width: 200,
            height: 200
        });

        this.feedService.start(true).subscribe(
            () => {
                this.feedService.subscribeToFeed(1);
                this.isLoadingOK = true;
                this.qrcode.makeCode('{type:pos,id:1}');
                this.feedService.addFeed.subscribe(
                    feed => {
                        console.log(feed);
                        this.wechatName = feed.WechatName;
                        this.wechatImageURL = feed.WechatImageUrl;
                        this.counterDown();
                        this.isCounterDown = true;
                    }
                );

            },
            error => console.log('Error on init: ' + error));
    }

    counterDown(): void {
        $('.clock').FlipClock(3, {
            clockFace: 'Counter',
            autoStart: true,
            countdown: true,
            callbacks: {
                stop: () => {
                    this.isLoginOK = true;
                }
            }
        });
    }

    isUserLoggedIn(): boolean {
        return this.membershipService.isUserAuthenticated();
    }

    getUserName(): string {
        if (this.isUserLoggedIn()) {
            var _user = this.membershipService.getLoggedInUser();
            return _user.Username;
        }
        else
            return 'Account';
    }

    logout(): void {
        this.membershipService.logout()
            .subscribe(res => {
                localStorage.removeItem('user');
            },
            error => console.error('Error: ' + error),
            () => { });
    }
}
