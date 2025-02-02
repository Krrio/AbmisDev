"use client";

import * as React from "react";
import { Badge } from "@/components/ui/badge";
import { Input } from "@/components/ui/input";
import { Popover, PopoverContent, PopoverTrigger } from "@/components/ui/popover";
import { Button } from "@/components/ui/button";
import { X } from "lucide-react";
import { ToastProvider, ToastViewport } from "@/components/ui/toast";
import { useToast } from "@/hooks/use-toast";

export function TagInput() {
  const [tags, setTags] = React.useState<string[]>([]); 
  const [inputValue, setInputValue] = React.useState(""); 
  const [suggestedTags, setSuggestedTags] = React.useState<string[]>([
    "Work",
    "Personal",
    "Urgent",
    "Meeting",
  ]); 
  const { toast } = useToast(); 

  // Dodaj nowy tag
  const addTag = (tag: string) => {
    if (tags.length >= 3) {
      toast({
        title: "Tag limit reached",
        description: "You can add up to 3 tags.",
        variant: "error",
      });
      return;
    }

    if (tag && !tags.includes(tag)) {
      setTags([...tags, tag]);
      setInputValue(""); 
    }
  };

  const removeTag = (tagToRemove: string) => {
    setTags(tags.filter((tag) => tag !== tagToRemove));
  };

  return (
    <ToastProvider>
      <div className="flex flex-wrap items-center gap-2">
        {tags.map((tag) => (
          <Badge key={tag} variant="outline" className="flex items-center gap-1">
            {tag}
            <button onClick={() => removeTag(tag)} className="ml-1">
              <X className="h-3 w-3" />
            </button>
          </Badge>
        ))}

        <Popover modal={true}>
          <PopoverTrigger asChild>
            <Button variant="outline" size="sm" className="h-8">
              + Add Tag
            </Button>
          </PopoverTrigger>
          <PopoverContent className="w-48 p-2">
            <div className="space-y-2">
              {/* Sugerowane tagi */}
              {suggestedTags.map((tag) => (
                <div
                  key={tag}
                  onClick={() => addTag(tag)}
                  className="cursor-pointer p-1 hover:bg-gray-100 rounded"
                >
                  {tag}
                </div>
              ))}

              <Input
                value={inputValue}
                onChange={(e) => setInputValue(e.target.value)}
                onKeyDown={(e) => {
                  if (e.key === "Enter" && inputValue.trim()) {
                    addTag(inputValue.trim());
                  }
                }}
                placeholder="Custom tag"
                className="h-8"
              />
            </div>
          </PopoverContent>
        </Popover>
      </div>

      {/* Toast */}
      <ToastViewport />
    </ToastProvider>
  );
}