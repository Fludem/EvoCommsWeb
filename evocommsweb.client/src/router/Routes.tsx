// src/Routes.tsx
import { Routes, Route } from 'react-router-dom'
import Layout from '@/components/Layout.tsx'

import About from '@/pages/About';
import RequireAuth from '@/auth/guards/RequireAuth'
import LoginPage from "@/pages/auth/Login.tsx";

export default function AppRoutes() {
    return (
        <Routes>
            <Route path="/" element={<Layout />}>
                {/* Public/home route */}
                <Route index element={<LoginPage />} />

                {/* Protected route(s) */}
                <Route element={<RequireAuth />}>
                    <Route path="about" element={<About />} />
                </Route>
            </Route>

            {/* Optionally, define a catch-all or 404 route */}
            <Route path="*" element={<div>Not Found</div>} />
        </Routes>
    )
}
