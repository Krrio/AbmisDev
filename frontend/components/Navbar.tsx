"use client"

import { authItems, navItems } from '@/app/constants'
import Link from 'next/link'
import { usePathname } from 'next/navigation'
import React from 'react'

const Navbar = () => {

    const pathname = usePathname();

  return (
    <header className='mt-16'>
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
                            <Link
                                href={item.url}
                                className={`relative group w-max ${
                                    isActive
                                    ? 'bg-white px-5 py-3 rounded-full drop-shadow duration-200 ease-in-out'
                                    : 'hover:cursor-pointer'
                                }`}
                                >
                                <span>{item.name}</span>
                                {!isActive && (
                                    <>
                                    <span className="absolute -bottom-1 left-1/2 w-0 transition-all h-0.5 bg-[#4461F2] group-hover:w-3/6"></span>
                                    <span className="absolute -bottom-1 right-1/2 w-0 transition-all h-0.5 bg-[#4461F2] group-hover:w-3/6"></span>
                                    </>
                                )}
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