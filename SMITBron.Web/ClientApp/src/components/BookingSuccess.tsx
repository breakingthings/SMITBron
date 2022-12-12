import * as React from "react";
import { Button, TextField, Stack, Box } from "@mui/material";
import { useNavigate, useParams } from "react-router-dom";

export const BookingSucces = () => {
  const { id } = useParams();
  const navigate = useNavigate();

  const handleOk = () => {
    navigate("/customer/booking")
  }

  return (
    <Box>
      <Stack spacing={3}>
        <TextField aria-readonly value={id}  label="Your booking id"></TextField>
        <Button onClick={handleOk} variant='contained'>Ok</Button>
      </Stack>
    </Box>
  );
};
