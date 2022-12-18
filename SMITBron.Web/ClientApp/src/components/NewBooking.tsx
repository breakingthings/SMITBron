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

import {
  FormContainer,
  TextFieldElement,
  DatePickerElement,
  SelectElement,
} from "react-hook-form-mui";

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
import { useForm } from "react-hook-form";
import { useNavigate } from "react-router-dom";
import { WrapApi } from "../helpers/ApiHelper";
import { MainContext } from "../context/MainContext";

export const NewBooking = () => {
  const [apartments, setApartments] = useState<HotelApartmentResult[]>([]);

  const navigate = useNavigate();

  const bookingApi = new BookingClient();
  const apartmentsApi = new ApartmentsClient();
  const mainContext = React.useContext(MainContext);

  const bookApartment = async (values: NewBookingModel) => {
    const apiResult = await WrapApi(bookingApi.post(values), mainContext, true);

    if (apiResult) {
      navigate(`/customer/bookingsuccess/${apiResult}`);
    }
  };

  const fetchFreeApartments = async (
    from: moment.Moment,
    to: moment.Moment
  ) => {
    const data = await apartmentsApi.get(from, to);
    setApartments(data.result);
  };

  const getFreeApartments = (
    startDate?: moment.Moment | null,
    endDate?: moment.Moment | null
  ) => {
    if (startDate && endDate) {
      fetchFreeApartments(startDate, endDate);
    }
  };

  const NewFormContainer = FormContainer<NewBookingModel>;

  const formContext = useForm<NewBookingModel>({});

  let startDateRef = React.useRef(null);
  let endDateRef = React.useRef(null);

  return (
    <LocalizationProvider dateAdapter={AdapterMoment}>
      <NewFormContainer
        onSuccess={(x) => {
          bookApartment(x);
        }}
        reValidateMode="onChange"
        formContext={formContext}
      >
        <Stack spacing={2}>
          <TextFieldElement
            required
            variant="standard"
            label="First name"
            name="firstname"
            validation={{
              minLength: {
                value: 3,
                message: "First name must be at least 3 digits",
              },
            }}
          />
          <TextFieldElement
            required
            variant="standard"
            label="Last name"
            name="lastname"
            validation={{
              minLength: {
                value: 3,
                message: "Last name must be at least 3 digits",
              },
            }}
          />

          <TextFieldElement
            required
            variant="standard"
            label="Id code"
            name="idCode"
            validation={{
              minLength: {
                value: 8,
                message: "Id code must be at least 8 digits",
              },
              pattern: {
                value: /^\d+$/,
                message: "Id code must be digits only",
              },
            }}
          />

          <TextFieldElement
            required
            variant="standard"
            label="E-mail"
            name="email"
            validation={{
              pattern: {
                value: /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/,
                message: "Email is not valid",
              },
            }}
          />
          <div
            style={{ height: 0, width: "100%", margin: 0 }}
            ref={startDateRef}
          ></div>
          <DatePickerElement
            required
            name="startDate"
            label="Start date"
            disablePast
            inputFormat="DD.MM.YYYY"
            PopperProps={{
              anchorEl: startDateRef.current,
              placement: "top",
            }}
            onChange={(x) => {
              getFreeApartments(x, formContext.getValues().endDate);
            }}
          />
          <div
            style={{ height: 0, width: "100%", margin: 0 }}
            ref={endDateRef}
          ></div>
          <DatePickerElement
            required
            name="endDate"
            label="End date"
            disablePast
            inputFormat="DD.MM.YYYY"
            PopperProps={{
              anchorEl: endDateRef.current,
              placement: "top",
            }}
            onChange={(x) => {
              getFreeApartments(formContext.getValues().startDate, x);
            }}
          />
          <SelectElement
            required
            label="Select room"
            id="room"
            name="apartmentId"
            options={apartments.map((x) => {
              return {
                id: x.id,
                label: `Rooms: ${x.numberOfRooms}, Beds: ${x.numberOfBeds}, Price: ${x.price}€`,
              };
            })}
          ></SelectElement>
          <Button
            variant="contained"
            type="submit"
            disabled={!formContext.formState.isValid}
          >
            Book
          </Button>
        </Stack>
      </NewFormContainer>
    </LocalizationProvider>
  );
};
