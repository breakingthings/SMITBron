import * as React from 'react';
import { createBrowserRouter, Navigate, redirect } from 'react-router-dom';
import { ViewBookings } from '../components/ViewBookings'
import { NewBooking } from '../components/NewBooking'
import { CancelBooking } from '../components/CancelBooking';


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
        path: '/customer/cancelbooking',
        element: <CancelBooking />
     },
    {
        path: '/hotel/bookings',
        element: <ViewBookings />
    }


  ], {
    
  });