// @ts-ignore
// import * as bootstrap from 'bootstrap'
import React from 'react'
import ReactDOM from 'react-dom/client'
import {Navigate, Route, Routes} from 'react-router-dom'
import App from './App.tsx'
import './index.scss'
import {ErrorsPage} from "./errors/ErrorsPage";
import {ChargeRoutes} from "./routing/ChargeRoutes";
import {BrowserRouter} from "react-router-dom";
import {IndexPage} from "./pages/IndexPage";
import {NoPage} from "./errors/NoPage";
import {RegisterRoutes} from "./routing/RegisterRoutes";

ReactDOM.createRoot(document.getElementById('root')!).render(
  <React.StrictMode>
      <BrowserRouter> {/*basename={PUBLIC_URL}*/}
          <Routes>
              <Route path="/" element={<App />}>
                  <Route index element={<IndexPage />} />
                  <Route path='error/*' element={<ErrorsPage />} />
                  <Route path='charges/*' element={<ChargeRoutes />} />
                  <Route path='auth/*' element={<RegisterRoutes />} />
                  <Route index element={<Navigate to='/' />} />
                  <Route path="*" element={<NoPage />} />
              </Route>
          </Routes>
      </BrowserRouter>
  </React.StrictMode>,
)
