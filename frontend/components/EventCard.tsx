import {
    AlertDialog,
    AlertDialogAction,
    AlertDialogCancel,
    AlertDialogContent,
    AlertDialogDescription,
    AlertDialogFooter,
    AlertDialogHeader,
    AlertDialogTitle,
    AlertDialogTrigger,
  } from "@/components/ui/alert-dialog"
import { Textarea } from "@/components/ui/textarea"

import { Input } from "./ui/input"
import { Separator } from "./ui/separator"
import { DatePickerDemo } from "./DatePickerCard"
import StatusSelector from "./StatusSelector"
import ImportanceCard from "./ImportanceCard"
import { DateTimePicker } from "./DateTimePicker"
import { TagInput } from "./TagInput"
  
const EventCard = () => {
  return (
    <AlertDialog>
    <AlertDialogTrigger>Open</AlertDialogTrigger>
    <AlertDialogContent className="max-w-sm sm:max-w-lg rounded-xl">
        <AlertDialogHeader>
        <AlertDialogTitle>
            <div className="flex flex-col">
                <div className="flex mb-4">
                    <Input className="text-xl md:text-xl border-none shadow-none cursor-pointer w-1/2 truncate mr-2"/>
                    <TagInput />
                </div>
                <div className="items-center flex justify-between space-x-2">
                    <ImportanceCard />
                    <StatusSelector />
                </div>
            </div>
        </AlertDialogTitle>
        <Separator />
        <div className="flex justify-between items-center">
            <DatePickerDemo  />
            <Separator orientation="vertical" className="ml-1 mr-1 h-7 justify-center items-center flex"/>
            <DateTimePicker />
        </div>
        <AlertDialogDescription>
            <span className="flex">Description</span>
            <Textarea className="text-black dark:text-white"/>
        </AlertDialogDescription>
        </AlertDialogHeader>
        <AlertDialogFooter>
        <AlertDialogCancel className="shad-cancel-button">Cancel</AlertDialogCancel>
        <AlertDialogAction className="shad-popover-button">Continue</AlertDialogAction>
        </AlertDialogFooter>
    </AlertDialogContent>
    </AlertDialog>
  )
}

export default EventCard