import Navbar from "@/components/Navbar";
import Image from "next/image";
import { authItems, gradient1, gradient2, heroImage, logOptions } from "../constants";
import { Separator } from "@/components/ui/separator";
import { Button } from "@/components/ui/button";
import Link from "next/link";
import { ModeToggle } from "@/components/ui/ModeToggle";
import { AuroraText } from "@/components/ui/aurora-text";
import { useRef } from "react";
import MotionHero from "@/components/MotionHero";

const Layout = async ({ children }: { children: React.ReactNode }) => {

  return (
    <main className="container mx-auto px-[80px]">
      <div className="hidden lg:block">
        <Navbar />
      </div>
      <div className="flex flex-col lg:flex-row w-full h-screen">
        <div className="lg:w-2/3 h-full flex flex-col justify-center items-center lg:items-start space-y-[40px]">
          <div className="flex flex-col lg:flex-row items-center lg:items-start lg:space-x-4 lg:-mt-[165px] mt-10">
            <div>
              <h1 className="md:text-4xl text-3xl font-bold hero-1 text-center lg:text-left">
                The Best Way <br className="hidden sm:block"/>to{' '}<AuroraText>Control Your Time</AuroraText>
              </h1>
              <Image 
                src={gradient2}
                alt="gradient"
                width={500}
                height={500}
                className="absolute -mt-[400px] -z-10 -ml-[220px] 2xl:-ml-[270px] dark:sm:hidden dark:lg:block"
                draggable={false}
              />
            </div>
            <div className="mt-4 lg:mt-0">
              <Image 
                src={heroImage}
                alt="hero"
                width={400}
                height={400}
                className="lg:-mt-6 lg:ml-4 2xl:ml-[90px] md:absolute md:hidden xl:block scale-75 lg:scale-100 hidden"
                draggable={false}
              />
              <div className="md:hidden">
                <MotionHero />
              </div>
            </div>
          </div>
          <div className="mt-4">
            <p className="text-md font-medium text-center lg:text-left mb-10 hidden md:block">
            Plan, organize, and take control<br />of your daily schedule
              <span className="font-bold text-[#4461F2]">{" "}effortlessly</span>
            </p>
            <Image 
              src={gradient1}
              alt="gradient"
              width={500}
              height={500}
              className="absolute -mt-[280px] hidden lg:block"
              draggable={false}
            />
          </div>
        </div>
        <div className="lg:w-1/3 h-full flex lg:justify-end justify-center">
          <div className="h-full flex flex-col justify-center items-end">
            <div className="flex w-full lg:px-0 flex-col items-end lg:flex-row lg:space-x-2 lg:-mt-[165px]">
              {children}
            </div>
            <div className="flex w-full items-center justify-center">
              <Separator className="bg-[#DFDFDF] items-center justify-center flex lg:max-w-[80%] md:max-w-[60%] mt-10" />
            </div>
            <div className="flex w-full items-center justify-center flex-row space-x-6 mt-10 mb-10 sm:mb-0">
              {logOptions.map((item, index) => (
                <Button
                  key={index}
                  className={`w-20 h-10 ${
                    item.num === 2 ? "selected-auth-option" : "auth-option"
                  }`}
                >
                  <Link href={item.url}>
                    <Image src={item.icon} alt={item.name} width={16} height={16} />
                  </Link>
                </Button>
              ))}
            </div>
          </div>
        </div>
      </div>
    </main>
  );
};

export default Layout;