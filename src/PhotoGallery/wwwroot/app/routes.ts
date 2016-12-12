import { ModuleWithProviders }  from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { HomeComponent } from './components/home.component';
import { CustomersComponent } from './components/customers.component';
import { accountRoutes, accountRouting } from './components/account/routes';


const appRoutes: Routes = [
    {
        path: '',
        redirectTo: '/customers',
        pathMatch: 'full'
    },
    {
        path: 'home',
        component: HomeComponent
    },
    {
        path: 'customers',
        component: CustomersComponent
    }
];

export const routing: ModuleWithProviders = RouterModule.forRoot(appRoutes);
