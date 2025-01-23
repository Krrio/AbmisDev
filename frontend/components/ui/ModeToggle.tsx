"use client";

import * as React from "react"
import { Moon, Sun } from "lucide-react"
import { useTheme } from "next-themes"
import { Switch } from "./switch";

// Prosty przełącznik tylko między Light a Dark
export function ModeToggle() {
  const { theme, setTheme } = useTheme()

  // next-themes wymaga sprawdzenia, czy jesteśmy po "hydration"
  // aby uniknąć błędów SSR/CSR (theme może być "undefined" na początku).
  const [mounted, setMounted] = React.useState(false)
  React.useEffect(() => {
    setMounted(true)
  }, [])

  // Dopóki komponent nie jest zamontowany, nie wyświetlaj Switcha
  if (!mounted) {
    return null
  }

  // Sprawdzamy, czy obecny theme to "dark"
  const isDark = theme === "dark"

  // Funkcja zmieniająca motyw
  const toggleTheme = (checked: boolean) => {
    setTheme(checked ? "dark" : "light")
  }

  return (
    <div className="flex items-center gap-2">
      {/* Ikona zależna od bieżącego motywu */}
      {isDark ? (
        <Moon className="h-5 w-5 text-white" />
      ) : (
        <Sun className="h-5 w-5 text-black" />
      )}
      {/* Switch z shadcn - przełącza motyw */}
      <Switch checked={isDark} onCheckedChange={toggleTheme} />
    </div>
  )
}
