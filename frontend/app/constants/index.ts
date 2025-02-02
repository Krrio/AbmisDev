// Hero image

export const heroImage = "/images/hero.png"

// Nav links

export const navItems = [
    {
      name: "Home",
      url: "/",
    },
    {
      name: "About",
      url: "/about",
    },
    {
      name: "Blog",
      url: "/blog",
    },
    {
      name: "Pages",
      url: "/pages",
    },
    {
      name: "Contact",
      url: "/contact",
    },
  ];

export const authItems = [
    {
        name: "Sign In",
        url: "/sign-in",
      },
      {
        name: "Register",
        url: "/sign-up",
      },
]

export const MobileNavItems = [
  {
    name: "Home",
    url: "/",
    icon: "/svg/blue1.svg"
  },
  {
    name: "About",
    url: "/about",
    icon: "/svg/blue2.svg"
  },
  {
    name: "Blog",
    url: "/blog",
    icon: "/svg/blue3.svg"
  },
  {
    name: "Pages",
    url: "/pages",
    icon: "/svg/blue4.svg"
  },
  {
    name: "Contact",
    url: "/contact",
    icon: "/svg/blue5.svg"
  },
];

export const MobileIconDark = "/svg/hamburger-dark.svg"
export const MobileIconLight = "/svg/hamburger-light.svg"


export const logOptions = [
  {
      name: "Google",
      url: "/sign-in",
      icon: "/images/google.png",
      num: 1
    },
    {
      name: "Apple",
      url: "/sign-up",
      icon: "/images/apple.png",
      num: 2
    },
    {
      name: "Facebook",
      url: "/sign-up",
      icon: "/images/facebook.png",
      num: 3
    },
]

export const gradient1 = "/svg/Ellipse1.svg"

export const gradient2 = "/svg/Ellipse2.svg"

export const sidebarImage= "/images/sidebarImage.png"

export const logoutIcon = "/svg/logout.svg"

export const dot = "/svg/dot-selected.svg"

export const MobileLogo = "/svg/mobileLogo.svg"

export const Logo = "/images/LogoIcon.png"

export const Logo1 = "/images/LogoIcon1.png"

interface Status {
  name: string;
  value: string;
}

export const Statuses: Status[] = [
  { name: "To Do", value: "To Do" },
  { name: "In Progress", value: "In Progress" },
  { name: "Done", value: "Done" },
];


interface Priority {
  name: string;
  value: string;
  icon: string;
}

export const Priorities: Priority[] = [
  { name: "Very High", value: "Very High", icon: "/svg/highestIcon.svg" },
  { name: "High", value: "High", icon: "/svg/highIcon.svg" },
  { name: "Medium", value: "Work", icon: "/svg/mediumIcon.svg" },
  { name: "Low", value: "Low", icon: "/svg/lowIcon.svg" },
  { name: "Lowest", value: "Lowest", icon: "/svg/lowestIcon.svg" },
];
