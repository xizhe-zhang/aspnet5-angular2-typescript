import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Feed } from '../interfaces';

@Injectable()
export class UtilityService {

    private _router: Router;
    private connectionID: string;
    private feed: Feed

    constructor(router: Router) {
        this._router = router;
    }

    convertDateTime(date: Date) {
        var _formattedDate = new Date(date.toString());
        return _formattedDate.toDateString();
    }

    navigate(path: string) {
        this._router.navigate([path]);
    }

    navigateToSignIn() {
        this.navigate('/account/login');
    }

    setConnectionId(connectionID: string) {
        this.connectionID = connectionID;
    }

    getConnectionId(): string {
        return this.connectionID;
    }

    setFeed(feed: Feed){
        this.feed = feed;
    }

    getFeed():Feed{
        return this.feed;
    }
}