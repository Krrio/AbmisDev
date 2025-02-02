"use client";

import * as React from "react";
import { ChevronDown } from "lucide-react";
import TimePicker from "react-time-picker";
import "react-time-picker/dist/TimePicker.css";
import "react-clock/dist/Clock.css";
import { Button } from "@/components/ui/button";
import {
  Popover,
  PopoverContent,
  PopoverTrigger,
} from "@/components/ui/popover";

export function DateTimePicker() {
  const [startTime, setStartTime] = React.useState<string>("12:00");
  const [endTime, setEndTime] = React.useState<string>("13:00");

  return (
    <div className="flex space-x-2">
      <Popover modal={true}>
        <PopoverTrigger asChild>
          <Button
            variant={"outline"}
            className="flex justify-center items-center font-normal"
          >
            <span>{startTime}</span>
            <ChevronDown className="h-4 w-4 sm:block hidden" />
          </Button>
        </PopoverTrigger>
        <PopoverContent className="w-auto p-2" align="start">
          <TimePicker
            onChange={setStartTime}
            value={startTime}
            disableClock
            clearIcon={null}
            className="custom-time-picker" // Dodaj klasę
          />
        </PopoverContent>
      </Popover>
      <span className="items-center justify-center flex text-sm text-gray-500">to</span>
      {/* Wybór godziny zakończenia */}
      <Popover modal={true}>
        <PopoverTrigger asChild>
          <Button
            variant={"outline"}
            className="flex justify-center items-center font-normal"
          >
            <span>{endTime}</span>
            <ChevronDown className="h-4 w-4 sm:block hidden" />
          </Button>
        </PopoverTrigger>
        <PopoverContent className="w-auto p-2" align="start">
          <TimePicker
            onChange={setEndTime}
            value={endTime}
            disableClock
            clearIcon={null}
            className="custom-time-picker" // Dodaj klasę
          />
        </PopoverContent>
      </Popover>
    </div>
  );
}