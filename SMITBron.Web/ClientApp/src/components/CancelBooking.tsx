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
import {
  FormContainer,
  TextFieldElement,
  DatePickerElement,
  SelectElement,
} from "react-hook-form-mui";
import { useForm } from "react-hook-form";
import { WrapApi } from "../helpers/ApiHelper";
import { MainContext } from "../context/MainContext";

export const CancelBooking = () => {
  const mainContext = React.useContext(MainContext);
  const bookingApi = new BookingClient();
  const cancelBooking = async (model: CancelBookingModel) => {
    var reqResult = await WrapApi(bookingApi.cancel(model), mainContext);
    if (reqResult !== null) {
      mainContext.showSnack("Booking canceled", "success");
    }
  };

  const formContext = useForm<CancelBookingModel>({});

  const CancelFormContainer = FormContainer<CancelBookingModel>;

  return (
    <div>
      <CancelFormContainer
        onSuccess={(model) => {
          cancelBooking(model);
        }}
        reValidateMode="onChange"
        formContext={formContext}
      >
        <Stack spacing={2}>
          <TextFieldElement
            variant="standard"
            label="Booking code"
            name="bookingId"
            required
          />
          <TextFieldElement
            variant="standard"
            label="Id code"
            name="idCode"
            required
          />
          <TextFieldElement
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
          <Button
            variant="contained"
            type="submit"
            disabled={!formContext.formState.isValid}
          >
            Cancel
          </Button>
        </Stack>
      </CancelFormContainer>
    </div>
  );
};
