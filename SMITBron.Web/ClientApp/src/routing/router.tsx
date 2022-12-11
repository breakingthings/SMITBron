import * as React from 'react';
import { createBrowserRouter, Navigate, redirect } from 'react-router-dom';
import { ViewBookings } from '../components/ViewBookings'
import { NewBooking } from '../components/NewBooking'


export const router = createBrowserRouter([
    {
        path: '/',
        element: <Navigate  replace to='/customer/booking'></Navigate>
     },
     {
        path: '/customer/booking',
        element: <NewBooking />
     },
    {
        path: '/hotel/bookings',
        element: <ViewBookings />
    }


  ]);