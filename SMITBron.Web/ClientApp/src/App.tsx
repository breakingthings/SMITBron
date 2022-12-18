import * as React from "react";
import { Layout }  from "./components/Layout";

import {
  ApartmentsClient,
  HotelApartmentResult,
  BookingClient,
  NewBookingModel,
} from "./APIClient";
import * as moment from "moment";
import "./custom.css";
import { useEffect, useState } from "react";
import { useFormik, FormikHelpers } from "formik";
import { router } from './routing/router';
import {
  RouterProvider
} from "react-router-dom";


export default () => {

  return (
    <Layout>
        <React.StrictMode>
          <RouterProvider router={router} />
        </React.StrictMode>
    </Layout>
  );
};
