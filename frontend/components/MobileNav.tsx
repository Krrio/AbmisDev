"use client"

import { MobileIconLight, MobileNavItems, sidebarImage } from "@/app/constants"
import {
    Sheet,
    SheetContent,
    SheetTitle,
    SheetTrigger,
  } from "@/components/ui/sheet"
import Image from "next/image"
import { Separator } from "./ui/separator"
import { cn } from "@/lib/utils"
import { usePathname } from "next/navigation"
import UserFooter from "./UserFooter"
import Link from "next/link"
  
const MobileNav = () => {

    const pathname = usePathname();

  return (
    <div className="flex items-center justify-between h-[50px]">
        <div>
            <p className="font-bold">AbmisDev</p>
        </div>
        <Sheet>
            <SheetTrigger>
                <Image src={MobileIconLight} alt="open" width={34} height={34} className="dark:invert"/>
            </SheetTrigger>
                <SheetContent>
                    <SheetTitle className="flex text-xl font-bold">
                        AbmisDev
                    </SheetTitle>
                    <Separator className="mt-4"/>
                    <nav className="flex px-4 mt-5">
                        <ul className="mobile-nav-list font-medium">
                        {MobileNavItems.map(({ url, name, icon }) => (
                                <Link key={name} href={url} className="lg:w-full">
                                <li
                                    className={cn(
                                    "mobile-nav-item",
                                    pathname === url && "shad-active",
                                    )}
                                >
                                    <Image
                                    src={icon}
                                    alt={name}
                                    width={24}
                                    height={24}
                                    className={cn(
                                        "nav-icon",
                                        pathname === url && "nav-icon-active",
                                    )}
                                    />
                                    <p>{name}</p>
                                </li>
                                </Link>
                            ))}
                            <Separator className="mt-4"/>
                        </ul>
                    </nav>
                    <div className="flex items-center justify-center mt-6">
                        <Image
                            src={sidebarImage}
                            alt="img"
                            width={224}
                            height={224} 
                            />
                        </div>
                        <Separator className="mt-8"/>
                        <div className="flex items-center justify-center mt-4">
                            <UserFooter />
                        </div>
            </SheetContent>
        </Sheet>
    </div>
  )
}

export default MobileNav