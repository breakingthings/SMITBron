import * as React from "react";
import { HotelClient, AllBookingsResult, Booking } from "../APIClient";
import * as moment from "moment";
import { useEffect, useState } from "react";
import {
  DataGrid,
  GridColumns,
  GridSortModel,
  GridSortItem,
  GridColDef,
  GridValueFormatterParams,
} from "@mui/x-data-grid";
import { Box, Grid } from "@mui/material";

export const ViewBookings = () => {
  const hotelApi = new HotelClient();
  const [allBookingsResult, setBookings] = useState<AllBookingsResult>(
    new AllBookingsResult({ bookings: [], totalCount: 0 })
  );
  const [sortModel, setSortModel] = useState<GridSortItem>({
    field: "",
    sort: "asc",
  });

  const getBookings = async () => {
    let swResult = await hotelApi.getAllBookings(
      1,
      100,
      false,
      sortModel.field,
      sortModel.sort === "desc"
    );
    setBookings(swResult.result);
  };

  const handleSortModelChange = React.useCallback(
    (sortModel: GridSortModel) => {
      setSortModel(sortModel[0]);
    },
    []
  );

  useEffect(() => {
    getBookings();
  }, []);

  const columns: GridColDef[] = [
    {
      field: "apartmentNumber",
      sortable: true,
      headerName: "Apt.",
    },

    {
      field: "startDate",
      sortable: true,
      headerName: "Start date",
      valueFormatter: (data: GridValueFormatterParams<moment.Moment>) =>
        data.value.format("DD.MM.yyyy"),
    },
    {
      field: "endDate",
      sortable: true,
      headerName: "End date",
      valueFormatter: (data: GridValueFormatterParams<moment.Moment>) =>
        data.value.format("DD.MM.yyyy"),
    },
    {
      field: "userFirstname",
      sortable: true,
      headerName: "First name",
    },
    {
      field: "userLastname",
      sortable: true,
      headerName: "Last name",
    },
    {
      field: "userEmail",
      sortable: true,
      headerName: "Email",
      maxWidth: 400,
    },
    {
      field: "userIdCode",
      sortable: true,
      headerName: "Id code",
      maxWidth: 400,
    },
    {
      field: "cancelDate",
      sortable: true,
      headerName: "Cancel date",
      maxWidth: 400,
    },
    {
      field: "totalPrice",
      sortable: true,
      headerName: "Total price",
      maxWidth: 400,
    },
  ];

  return (
    <DataGrid
      autoHeight={true}
      columns={columns}
      rows={allBookingsResult?.bookings ?? new Array<Booking>()}
      sortingMode="server"
      onSortModelChange={handleSortModelChange}
      pageSize={10}
      rowsPerPageOptions={[5, 10, 50, 100]}
    ></DataGrid>
  );
};
