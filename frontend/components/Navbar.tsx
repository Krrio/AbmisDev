"use client"

import { authItems, dot, navItems } from '@/app/constants'
import Link from 'next/link'
import { usePathname } from 'next/navigation'
import React, { useEffect, useRef, useState } from 'react'
import Logout from './Logout'
import { ModeToggle } from './ui/ModeToggle'
import Image from 'next/image'
import { motion } from "framer-motion";

const Navbar = () => {

    const pathname = usePathname();

     // Stan przechowujący informację, czy user jest zalogowany
  const [isLoggedIn, setIsLoggedIn] = useState<boolean>(false);

    useEffect(() => {
        // Po załadowaniu komponentu (po stronie klienta),
        // sprawdzamy, czy w localStorage znajduje się token
        const token = localStorage.getItem("token");
        if (token) {
        setIsLoggedIn(true);
        } else {
        setIsLoggedIn(false);
        }
    }, []);

    const ref = useRef<HTMLUListElement>(null);
    const [left, setLeft] = useState(0);
    const [width, setWidth] = useState(0);
    const [opacity, setOpacity] = useState(0);
  
    const handleMouseEnter = (e: React.MouseEvent<HTMLLIElement>) => {
      const node = e.currentTarget;
      const rect = node.getBoundingClientRect();
      setLeft(node.offsetLeft);
      setWidth(rect.width);
      setOpacity(1);
    };
  
    const handleMouseLeave = () => {
      setOpacity(0);
    };

  return (
    <header className='mt-16'>
        <div className='flex items-center justify-between cursor-pointer'>
            <nav className='relative'>
            {isLoggedIn && (
                <ul ref={ref} onMouseLeave={handleMouseLeave} className='flex space-x-8 font-medium header-1'>
                {navItems.map((item, index) => {
                    const isActive = pathname === item.url;                  
                    return (
                        <li key={index} className="relative" onMouseEnter={handleMouseEnter}>
                        <Link href={item.url} className="px-2 py-1 flex flex-col items-center justify-center">
                          {item.name}
                          {isActive && (
                            <Image
                              src={dot}
                              alt="dot"
                              width={16}
                              height={16}
                              className='dark:invert'
                            />
                          )}
                        </Link>
                      </li>
                    );
                })}
                <motion.li
                  animate={{ left, width, opacity }}
                  transition={{ type: "spring", stiffness: 400, damping: 30 }}
                  className="absolute top-[26.5px] z-0 items-center justify-center flex -translate-x-8">
                    <Image
                      src={dot}
                      alt="dot"
                      width={16}
                      height={16}
                      className='dark:invert flex items-center justify-center'
                    />
                  </motion.li>
                </ul>
            )}
            </nav>
            <nav>
                <ul className='flex space-x-8 header-1 font-medium text-[#4461F2]'>
                    {isLoggedIn ? (
                        <>
                        <li className='flex'>
                          <Logout />
                          <ModeToggle />
                        </li>
                      </>
                    ) : ( 
                    authItems.map((item, index) => {
                        const isActive = pathname === item.url;
                        return (
                        <li key={index}>
                            <Link
                                href={item.url}
                                className={`relative group w-max font-bold ${
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
                    }))}
                </ul>
            </nav>
        </div>
    </header>
  )
}

export default Navbar