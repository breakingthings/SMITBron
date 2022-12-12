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
  NewBookingModel,
} from "../APIClient";
import * as moment from "moment";
import { useEffect, useState } from "react";
import { useFormik, FormikHelpers } from "formik";
import { useNavigate } from "react-router-dom";

export const NewBooking = () => {

  const [apartments, setApartments] = useState<HotelApartmentResult[]>([]);

  const navigate = useNavigate();

  const bookingApi = new BookingClient();
  const apartmentsApi = new ApartmentsClient();

  const bookApartment = async (
    values: NewBookingModel,
    helpers: FormikHelpers<NewBookingModel>
  ) => {
    const apiResult = await bookingApi.post(values);
    if(apiResult.status === 200){
      navigate(`/customer/bookingsuccess/${apiResult.result}`)
    }    
  };


  const fetchFreeApartments = async (
    from: moment.Moment,
    to: moment.Moment
  ) => {
    const data = await apartmentsApi.get(from, to);
    setApartments(data.result);
  };

  const formik = useFormik<NewBookingModel>({
    initialValues: new NewBookingModel(),
    onSubmit: bookApartment,
    validateOnChange: true,
    validate: (values) => {
      return {};
    },
  });

  const getFreeApartments = (startDate?: moment.Moment | null, endDate?: moment.Moment | null) => {
    if (startDate && endDate) {
      fetchFreeApartments(startDate, endDate);
    }
  };

  return (
    <LocalizationProvider dateAdapter={AdapterMoment}>
      <form onSubmit={formik.handleSubmit}>
        <Stack spacing={2}>
          <TextField
            variant="standard"
            label="First name"
            name="firstname"
            value={formik.values.firstname}
            onChange={formik.handleChange}
          />
          <TextField
            variant="standard"
            label="Last name"
            name="lastname"
            value={formik.values.lastname}
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
          <DesktopDatePicker
            label="Start date"
            disablePast
            inputFormat="DD.MM.YYYY"
            value={formik.values.startDate ?? null}
            onChange={async (value) => {
              await formik.setFieldValue("startDate", value);
              getFreeApartments(value, formik.values.endDate);
            }}
            renderInput={(params) => <TextField {...params} />}
          />

          <DesktopDatePicker
            label="End date"
            disablePast
            inputFormat="DD.MM.YYYY"
            value={formik.values.endDate ?? null}
            onChange={async (value) => {
              await formik.setFieldValue("endDate", value);
              getFreeApartments(formik.values.startDate, value);
            }}
            renderInput={(params) => <TextField {...params} />}
          />

          <FormControl fullWidth>
            <InputLabel id="demo-simple-select-label">Room</InputLabel>
            <Select
              labelId="Select room"
              id="room"
              label="Select room"
              value={formik.values.apartmentId}
              onChange={formik.handleChange}
              name="apartmentId"
            >
              {apartments.map((x) => (
                <MenuItem value={x.id}>
                  Rooms: {x.numberOfRooms}, Beds: {x.numberOfBeds}, Price:{" "}
                  {x.price}€
                </MenuItem>
              ))}
            </Select>
          </FormControl>
          <Button variant="contained" type="submit" disabled={!formik.isValid}>
            Book
          </Button>
        </Stack>
      </form>
    </LocalizationProvider>
  );
};
