import * as React from "react";
import {
  Button,
  FormControl,
  Select,
  InputLabel,
  MenuItem,
  TextField,
  Stack,
} from "@mui/material";

import { DesktopDatePicker, LocalizationProvider } from "@mui/x-date-pickers";
import { AdapterMoment } from "@mui/x-date-pickers/AdapterMoment";
import {
  ApartmentsClient,
  HotelApartmentResult,
  BookingClient,
  CancelBookingModel,
} from "../APIClient";
import * as moment from "moment";
import { useEffect, useState } from "react";
import { useFormik, FormikHelpers } from "formik";


export const CancelBooking = () => {

    const bookingApi = new BookingClient();

    const cancelBooking = async (model: CancelBookingModel) => {
        var reqResult = await bookingApi.cancel(model);
        if(reqResult.status === 200){
            alert('Booking canceled');
        }
    }

    const formik = useFormik<CancelBookingModel>({
        initialValues: new CancelBookingModel(),
        onSubmit: cancelBooking,
        validateOnChange: true,
        validate: (values) => {
          return {};
        },
      });

    return ( <form onSubmit={formik.handleSubmit}>
        <Stack spacing={2}>
        <TextField
            variant="standard"
            label="Booking code"
            name="bookingId"
            value={formik.values.bookingId}
            onChange={formik.handleChange}
          />
          <TextField
            variant="standard"
            label="Id code"
            name="idCode"
            value={formik.values.idCode}
            onChange={formik.handleChange}
          />
          <TextField
            variant="standard"
            label="E-mail"
            name="email"
            value={formik.values.email}
            onChange={formik.handleChange}
          />
          <Button variant="contained" type="submit" disabled={!formik.isValid}>
            Cancel
          </Button>

           </Stack>
          </form>);
}