import * as React from "react";
import {
  Container,
  createTheme,
  CssBaseline,
  ThemeProvider,
  Toolbar,
  AlertColor,
  CircularProgress,
  Snackbar,
  Box,
  Link,
  BottomNavigation,
  BottomNavigationAction,
  Drawer,
  List,
  ListItem,
  ListItemText,
} from "@mui/material";

import { IFetchTime, IMainContext, MainProvider } from "../context/MainContext";
import MainStyles from "../styles/Main.module.css";
import AppBar from "@mui/material/AppBar";
import Alert from "@mui/material/Alert";
import RestoreIcon from "@mui/icons-material/Restore";

export const Layout = (props: { children?: React.ReactNode }) => {
  const theme = createTheme();

  const [snackInfo, setSnackInfo] = React.useState<{
    isOpen: boolean;
    content: string;
    severity: AlertColor;
  }>({ isOpen: false, content: "", severity: "info" });

  const [drawerOpen, setDrawerOpen] = React.useState<boolean>(false);

  const closeDrawer = () => {
    setDrawerOpen(false);
  };

  const fetchCountReducer = (state: { count: number }, isFetching: boolean) => {
    return isFetching === true
      ? { ...state, count: state.count + 1 }
      : { ...state, count: state.count === 0 ? 0 : state.count - 1 };
  };

  const fetchTimesReducer = (
    state: { times: IFetchTime[] },
    lastFetchTime: IFetchTime
  ) => {
    let stateTimes = [...state.times];
    if (stateTimes.length > 9) {
      stateTimes.shift();
    }
    stateTimes.push(lastFetchTime);

    return { ...state, times: stateTimes };
  };

  const showSnack = (content: string, type: AlertColor) => {
    setSnackInfo({ isOpen: true, content: content, severity: type });
  };

  const [fetchingCount, dispatchFetching] = React.useReducer(
    fetchCountReducer,
    {
      count: 0,
    }
  );

  const [lastFetchTimes, dispatchLastFetchTime] = React.useReducer(
    fetchTimesReducer,
    { times: [] }
  );

  const contextActions: IMainContext = {
    showSnack: showSnack,
    setLoading: (loading: boolean) => {
      dispatchFetching(loading);
    },

    setLastFetchTime: (time: IFetchTime) => {
      dispatchLastFetchTime(time);
    },
  };

  const closeSnackbar = () => {
    setSnackInfo({ isOpen: false, content: "", severity: "error" });
  };

  const Snack = () => (
    <Snackbar
      anchorOrigin={{
        vertical: "bottom",
        horizontal: "left",
      }}
      open={snackInfo.isOpen}
      autoHideDuration={10000}
      onClose={closeSnackbar}
    >
      <Alert
        elevation={4}
        severity={snackInfo.severity}
        onClose={closeSnackbar}
      >
        {snackInfo.content}
      </Alert>
    </Snackbar>
  );

  return (
    <ThemeProvider theme={theme}>
      <Container component="main">
        <Snack />
        <MainProvider value={contextActions}>
          <AppBar position="static">
            <Container>
              <Toolbar>
                <Box sx={{ flexGrow: 1, display: { sm: "flex" } }}>
                  <Link
                    sx={{ color: "white", display: "block", my: 2, marginX: 2 }}
                    href="/customer/booking"
                    variant="button"
                  >
                    Add booking
                  </Link>

                  <Link
                    sx={{ color: "white", display: "block", my: 2 }}
                    href="/customer/cancelbooking"
                    variant="button"
                  >
                    Cancel booking
                  </Link>
                </Box>
              </Toolbar>
            </Container>
          </AppBar>
          <br />
          <CssBaseline />

          {fetchingCount.count > 0 ? (
            <div className={MainStyles.waitingOverlay}>
              <CircularProgress className={MainStyles.waitSpinner} />
            </div>
          ) : (
            <React.Fragment />
          )}
          {props.children}

          <BottomNavigation sx={{ width: 500, marginTop: "10px" }}>
            <BottomNavigationAction
              label="Queries"
              value="queries"
              icon={<RestoreIcon />}
              onClick={() => {
                setDrawerOpen(true);
              }}
            ></BottomNavigationAction>
          </BottomNavigation>
          <Drawer anchor="bottom" open={drawerOpen} onClose={closeDrawer}>
            <List>
              {lastFetchTimes.times.map((item, index) => {
                return (
                  <ListItem key={index}>
                    <ListItemText>
                      {item.call} {item.milliSeconds}ms
                    </ListItemText>
                  </ListItem>
                );
              })}
            </List>
          </Drawer>
        </MainProvider>
      </Container>
    </ThemeProvider>
  );
};
