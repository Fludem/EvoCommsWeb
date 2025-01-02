import { Alert, AlertTitle, AlertDescription } from '@/components/ui/alert'
import { Button } from '@/components/ui/button'

export default function About() {
    return (
        <div className="p-4">
            <Alert>
                <AlertTitle>About Our Company</AlertTitle>
                <AlertDescription>
                    We build awesome web applications with React and ASP.NET Core.
                </AlertDescription>
            </Alert>
            <div className="mt-4">
                <Button variant="outline">Learn More</Button>
            </div>
        </div>
    )
}
