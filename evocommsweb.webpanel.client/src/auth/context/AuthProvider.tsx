import React, { createContext, useContext, useEffect, useState } from 'react'

interface AuthContextProps {
    isAuthenticated: boolean
    isLoading: boolean
    // add more user info if needed, e.g. user roles, etc.
}

const AuthContext = createContext<AuthContextProps>({
    isAuthenticated: false,
    isLoading: true,
})

export function AuthProvider({ children }: { children: React.ReactNode }) {
    const [isAuthenticated, setIsAuthenticated] = useState(false)
    const [isLoading, setIsLoading] = useState(true)

    useEffect(() => {
        // Example: call an ASP.NET Core endpoint to check if user is authenticated
        // This might be something like: GET /api/auth/isauthenticated
        // which returns { isAuthenticated: true/false }

        fetch('/api/auth/isauthenticated', {
            method: 'GET',
            credentials: 'include', // so cookies are sent
        })
            .then((res) => res.json())
            .then((data) => {
                setIsAuthenticated(data.isAuthenticated)
                setIsLoading(false)
            })
            .catch((err) => {
                console.error(err)
                setIsAuthenticated(false)
                setIsLoading(false)
            })
    }, [])

    return (
        <AuthContext.Provider value={{ isAuthenticated, isLoading }}>
            {children}
        </AuthContext.Provider>
    )
}

export function useAuth() {
    return useContext(AuthContext)
}
