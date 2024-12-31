import { Navigate, Outlet, useLocation } from 'react-router-dom'
import { useAuth } from '@/auth/context/AuthProvider'

export default function RequireAuth() {
    const { isAuthenticated, isLoading } = useAuth()
    const location = useLocation()

    // While we’re checking auth status, maybe show a spinner or loading screen
    if (isLoading) {
        return <div>Loading...</div>
    }

    // If not authenticated, redirect to login (or home, or wherever)
    if (!isAuthenticated) {
        return <Navigate to="/" state={{ from: location }} replace />
    }

    // If authenticated, render child routes
    return <Outlet />
}
