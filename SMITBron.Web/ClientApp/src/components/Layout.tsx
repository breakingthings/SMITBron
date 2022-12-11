import * as React from "react";
import {
  Container,
  createTheme,
  CssBaseline,
  ThemeProvider,
  Toolbar,
} from "@mui/material";

import AppBar from '@mui/material/AppBar';

const theme = createTheme();

export default (props: { children?: React.ReactNode }) => (
  <ThemeProvider theme={theme}>
    <Container component="main">
      <AppBar position='static'>
        <Container>
          <Toolbar></Toolbar>
        </Container>
        </AppBar>
        <br />
      <CssBaseline />

      {props.children}
    </Container>
  </ThemeProvider>
);
