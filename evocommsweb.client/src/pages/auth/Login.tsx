import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";
import { ScanFace } from 'lucide-react'

export default function LoginPage() {
    return (
        <div className="flex w-full h-full flex-col items-center justify-center gap-6">
        <div className="w-full max-w-sm">
            <div className="flex flex-col gap-6">
                <form>
                    <div className="flex flex-col gap-6">
                        <div className="flex flex-col items-center gap-2">
                            <a
                                href="#"
                                className="flex flex-col items-center gap-2 font-medium"
                            >
                                <div className="flex size-14 items-center justify-center rounded-md">
                                    <ScanFace className="size-12 text-primary"/>
                                </div>
                                <span className="sr-only">Evocomms</span>
                            </a>
                            <h1 className="text-2xl font-bold">Welcome to <span className="text-primary">EvoComms</span></h1>
                        </div>
                        <div className="flex flex-col gap-6">
                            <div className="grid gap-2">
                                <Label htmlFor="username">Username</Label>
                                <Input
                                    id="username"
                                    type="text"
                                    placeholder="johndoe"
                                    required
                                />
                            </div>
                            <div className="grid gap-2">
                                <Label htmlFor="password">Password</Label>
                                <Input
                                    id="password"
                                    type="password"
                                    placeholder="password"
                                    required
                                />
                            </div>
                            <Button type="submit" className="w-full">
                                Login
                            </Button>
                        </div>
                        <div
                            className="relative text-center text-sm after:absolute after:inset-0 after:top-1/2 after:z-0 after:flex after:items-center after:border-t after:border-border">
            <span className="relative z-10 bg-background px-2 text-muted-foreground">
              Or
            </span>
                        </div>
                        <div className="grid gap-4 sm:grid-cols-1">
                            <Button variant="outline" size="sm" className="w-full">
                                <svg xmlns="http://www.w3.org/2000/svg" x="0px" y="0px" width="120" height="120"
                                     viewBox="0 0 48 48" className={ "!size-4 flex-shrink-0"}>
                                    <path fill="#00b0ff"
                                          d="M20 25.026L5.011 25 5.012 37.744 20 39.818zM22 25.03L22 40.095 42.995 43 43 25.066zM20 8.256L5 10.38 5.014 23 20 23zM22 7.973L22 23 42.995 23 42.995 5z"></path>
                                </svg>
                                Login with Windows Hello
                            </Button>
                        </div>
                    </div>
                </form>
                <div
                    className="text-balance text-center text-xs text-muted-foreground [&_a]:underline [&_a]:underline-offset-4 hover:[&_a]:text-primary  ">
                    Forgot your login? <span className="font-medium text-primary">Contact Support</span>
                </div>
            </div>
        </div>
        </div>
    )
                }