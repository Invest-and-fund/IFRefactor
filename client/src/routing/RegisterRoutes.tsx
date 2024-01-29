import {FC, lazy, ReactNode, Suspense} from 'react'
import {Navigate, Route, Routes} from 'react-router-dom'


// Define a fallback component
const LoadingView = () => <div>Loading...</div>;

const RegisterRoutes = () => {
    const LoginPage = lazy(() => import('../auth/pages/LoginPage'))
    const RegisterPage = lazy(() => import('../auth/pages/RegisterPage'))
    return (
        <Routes>
            {/*<Route path='' element={<ChargesIndexPage />} />*/}
            <Route
                path='register'
                element={
                    <Suspense fallback={<LoadingView />}>
                        <RegisterPage />
                    </Suspense>
                }
            />
            <Route
                path='login'
                element={
                    <Suspense fallback={<LoadingView />}>
                        <LoginPage />
                    </Suspense>
                }
            />
            {/* Page Not Found */}
            <Route path='*' element={<Navigate to='/error/404' />} />
        </Routes>
    )
}
export {RegisterRoutes}
