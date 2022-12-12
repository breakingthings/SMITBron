import * as React from "react";
import { createBrowserRouter, Navigate, redirect } from "react-router-dom";
import { ViewBookings } from "../components/ViewBookings";
import { NewBooking } from "../components/NewBooking";
import { CancelBooking } from "../components/CancelBooking";
import { BookingSucces } from "../components/BookingSuccess";

export const router = createBrowserRouter(
  [
    {
      path: "/",
      element: <Navigate replace to="/customer/booking"></Navigate>,
    },
    {
      path: "/customer/booking",
      element: <NewBooking />,
    },
    {
      path: "/customer/cancelbooking",
      element: <CancelBooking />,
    },
    {
      path: "/customer/bookingsuccess/:id",
      element: <BookingSucces />,
    },
    {
      path: "/hotel/bookings",
      element: <ViewBookings />,
    },
  ],
  {}
);
