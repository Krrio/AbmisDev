"use client";

import { z } from "zod";
import { zodResolver } from "@hookform/resolvers/zod";
import { useForm } from "react-hook-form";
import { Button } from "@/components/ui/button";
import {
  Form,
  FormControl,
  FormField,
  FormItem,
  FormMessage,
} from "@/components/ui/form";
import { Input } from "@/components/ui/input";
import Link from "next/link";
import { apiRequest } from "@/utils/api";
import { useRouter } from 'next/navigation'


// Typ formularza
type FormType = "sign-in" | "sign-up";

// Schemat walidacji Zod
const authFormSchema = (formType: FormType) => {
  return z.object({
    email: z.string().email("Invalid email address"),
    password: z.string().min(6, "Password must be at least 6 characters long"),
    fullName:
      formType === "sign-up"
        ? z.string().min(2, "Name must be at least 2 characters").max(50)
        : z.string().optional(),
  });
};

const AuthForm = ({ formType = "sign-up" }: { formType: FormType }) => {
  const schema = authFormSchema(formType);
  const form = useForm({
    resolver: zodResolver(schema),
    defaultValues: {
      email: "",
      password: "",
      fullName: formType === "sign-up" ? "" : undefined,
    },
  });

  const router = useRouter();
  
  const onSubmit = async (data: any) => {


    try {
      const endpoint =
        formType === "sign-up" ? "/auth/register" : "/auth/login";
  
      const payload =
        formType === "sign-up"
          ? { ...data, confirmPassword: data.password }
          : data;
  
      const result = await apiRequest(endpoint, "POST", payload);
  
      if (formType === "sign-in") {
        localStorage.setItem("token", result.token);
        alert("Login successful!");
        router.push("/"); // Przykładowe przekierowanie
      } else {
        alert("Registration successful!");
        router.push("/sign-in"); // Przykładowe przekierowanie
      }
    } catch (error: any) {
      console.error("Error:", error.message);
      alert(error.message || "An error occurred");
    }
  };
  

  return (
    <Form {...form}>
      <form onSubmit={form.handleSubmit(onSubmit)} className="w-full max-w-sm space-y-4">
        <FormField
          name="email"
          control={form.control}
          render={({ field }) => (
            <FormItem>
              <FormControl>
                <Input className="shad-form-input font-medium" type="email" placeholder="Enter your email" {...field} />
              </FormControl>
              <FormMessage />
            </FormItem>
          )}
        />
        <FormField
          name="password"
          control={form.control}
          render={({ field }) => (
            <FormItem>
              <FormControl>
              <Input
          type="password"
          placeholder="Enter your password"
          className="shad-form-input font-medium"
          {...field}
        />
              </FormControl>
              <FormMessage />
            </FormItem>
          )}
        />
        {formType === "sign-in" && (
        <div className="form-subtitle flex items-center justify-between text-sm space-x-16">
          <Link href="/recover" className="text-gray-400 hover:underline">
            Recover password?
          </Link>
          <Link href="/sign-up" className="lg:hidden block text-gray-400 hover:underline">
            Create an account?
          </Link>
        </div>
      )}
        {formType === "sign-up" && (
          <FormField
            name="fullName"
            control={form.control}
            render={({ field }) => (
              <FormItem>
                <FormControl>
                <Input
                    type="password"
                    placeholder="Enter your password"
                    className="shad-form-input font-medium"
                    {...field}
                  />
                </FormControl>
                <FormMessage />
              </FormItem>
            )}
          />
        )}
        {formType === "sign-up" && (
        <div className="form-subtitle-1 flex items-center justify-center text-sm">
          <Link href="/sign-in" className="lg:hidden block text-gray-400 hover:underline">
            Already have an account? <span className="text-[#4461F2] font-bold">Log in</span>
          </Link>
        </div>
      )}
        <Button type="submit" className="w-full shad-form-button">
          {formType === "sign-up" ? "Register" : "Sign In"}
        </Button>
      </form>
    </Form>
  );
};

export default AuthForm;
