import {FC, lazy, ReactNode, Suspense} from 'react'
import {Navigate, Route, Routes} from 'react-router-dom'


// Define a fallback component
const LoadingView = () => <div>Loading...</div>;

const ChargeRoutes = () => {
    const CreateChargePage = lazy(() => import('../pages/charge/CreateChargePage'))
    const ChargesIndexPage = lazy(() => import('../pages/charge/ChargesIndexPage'))
    return (
        <Routes>
            {/*<Route path='' element={<ChargesIndexPage />} />*/}
            <Route
                path=''
                element={
                    <Suspense fallback={<LoadingView />}>
                        <ChargesIndexPage />
                    </Suspense>
                }
            />
            <Route
                path='create'
                element={
                    <Suspense fallback={<LoadingView />}>
                        <CreateChargePage />
                    </Suspense>
                }
            />
            {/* Page Not Found */}
            <Route path='*' element={<Navigate to='/error/404' />} />
        </Routes>
    )
}
export {ChargeRoutes}
