"use client"

import { authItems, navItems } from '@/app/constants'
import Link from 'next/link'
import { usePathname } from 'next/navigation'
import React from 'react'
import { ModeToggle } from './ui/ModeToggle'

const Navbar = () => {

    const pathname = usePathname();

  return (
    <header className='mt-16'>
       <ModeToggle/>
        <div className='flex items-center justify-between'>
            <nav>
                <ul className='flex space-x-8 font-medium header-1'>
                    {navItems.map((item, index) => (
                        <li key={index}>
                            <Link href={item.url}>
                            {item.name}
                            </Link>
                        </li>
                    ))}
                </ul>
            </nav>
            <nav>
                <ul className='flex space-x-8 header-1 font-bold text-[#4461F2]'>
                    {authItems.map((item, index) => {
                        const isActive = pathname === item.url;
                        return (
                        <li key={index}>
                            <Link href={item.url} className={isActive ? 'bg-white px-5 py-3 rounded-full drop-shadow  duration-200 ease-in-out' : 'hover:underline decoration-[3px] underline-offset-[6px]'}>
                            {item.name}
                            </Link>
                        </li>
                        );
                    })}
                </ul>
            </nav>
        </div>
    </header>
  )
}

export default Navbar