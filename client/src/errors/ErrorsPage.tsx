/* eslint-disable jsx-a11y/anchor-is-valid */
import {Outlet, Route, Routes} from 'react-router-dom'
import {Error500} from './components/Error500'
import {Error404} from './components/Error404'

const ErrorsPage = () => (
  <Routes>
    <Route path='404' element={<Error404 />} />
    <Route path='500' element={<Error500 />} />
    <Route index element={<Error404 />} />
  </Routes>
)

export {ErrorsPage}
