import { Statuses } from "@/app/constants"
import {
    Select,
    SelectContent,
    SelectItem,
    SelectTrigger,
    SelectValue,
  } from "@/components/ui/select"
  
  const StatusSelector = () => {
    return (
      <Select>
        <SelectTrigger className="w-1/2">
          <SelectValue placeholder="Choose status" />
        </SelectTrigger>
        <SelectContent>
          {Statuses.map((status) => (
            <SelectItem key={status.value} value={status.value}>
              {status.name}
            </SelectItem>
          ))}
        </SelectContent>
      </Select>
    );
  };
  
  export default StatusSelector;
  