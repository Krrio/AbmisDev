"use client";

import { heroImage } from '@/app/constants';
import Image from 'next/image';
import React, { useEffect, useRef } from 'react';
import gsap from 'gsap';

const MotionHero = () => {
  const heroImageRef = useRef<HTMLDivElement>(null);

  // Fragment GSAP do animacji elementów UI 
  useEffect(() => {
    if (heroImageRef.current) {
      gsap.fromTo(
        heroImageRef.current,
        { y: -200, opacity: 0 }, 
        { y: 0, opacity: 1, duration: 1, ease: "power2.out" } 
      );
    }
  }, []);

  return (
    <div ref={heroImageRef} className="mt-4 lg:mt-0">
      <Image 
        src={heroImage}
        alt="hero"
        width={400}
        height={400}
        className="lg:-mt-6 lg:ml-4 md:absolute md:hidden xl:block scale-75 lg:scale-100"
        draggable={false}
      />
    </div>
  );
};

export default MotionHero;
