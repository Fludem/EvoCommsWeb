import { Outlet } from 'react-router-dom'

export default function Layout() {
    return (
        <div className="flex flex-col h-screen min-h-screen bg-background">
            
            <main className="w-full h-full p-2 md:p-4">
                <Outlet />
            </main>

            <footer className="bg-white border-t p-4 text-center">
                <p className="text-xs text-muted-foreground font-light">
                    &copy; {new Date().getFullYear()} Clocking Systems Ltd. All rights reserved.
                </p>
            </footer>
        </div>
    )
}
