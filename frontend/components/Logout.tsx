/* eslint-disable @typescript-eslint/no-unused-vars */
"use client";

import React from "react";
import { logout } from "@/utils/api";
import { useRouter } from "next/navigation";
import Image from "next/image";
import { logoutIcon } from "@/app/constants";
import { useToast } from "@/hooks/use-toast";

const Logout: React.FC = () => {
  const router = useRouter();
  const { toast } = useToast(); 

  const handleLogout = async () => {
    try {
      await logout(); 
      toast({
        title: "You have logged out successfully",
        description: "You have been logged out of your account.",
        variant: "confirm", 
      });
      router.push("/sign-in");
    } catch (error: unknown) {
      const errorMessage =
        error instanceof Error ? error.message : "Unknown error occurred.";
      toast({
        title: "Logout Error",
        description: `An error occurred during logout: ${errorMessage}`,
        variant: "destructive", 
      });
    }
  };

  return (
    <div className="group relative flex justify-center items-center w-48 px-2 py-1">
      <button
        onClick={handleLogout}
        className="logout-button-basic"
      >
        Log out
      </button>

      <div className="logout-button-anim">
        <Image
          src={logoutIcon}
          alt="logoutIcon"
          width={16}
          height={16}
          className="dark:invert"
        />
      </div>
    </div>
  );
};

export default Logout;
