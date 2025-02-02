import { Priorities } from "@/app/constants";
import {
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue,
} from "@/components/ui/select";
import Image from "next/image";

const ImportanceCard = () => {
  return (
    <Select>
      <SelectTrigger className="w-1/2">
        <SelectValue placeholder="Choose priority" />
      </SelectTrigger>
      <SelectContent className="w-[222px]">
        {Priorities.map((priority) => (
          <SelectItem
            key={priority.value}
            value={priority.value}
            className="flex items-center space-x-2"
          >
            <div className="flex items-center space-x-2">
              <Image
                src={priority.icon}
                alt={priority.name}
                width={30}
                height={30}
              />
              <span>{priority.name}</span>
            </div>
          </SelectItem>
        ))}
      </SelectContent>
    </Select>
  );
};

export default ImportanceCard;
