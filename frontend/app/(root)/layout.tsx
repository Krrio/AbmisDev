import { Footer } from "@/components/Footer";
import MobileNav from "@/components/MobileNav";
import Navbar from "@/components/Navbar";

const Layout = async ({ children }: { children: React.ReactNode }) => {

    return (
      <main className="container mx-auto px-[20px] sm:px-[80px] border border-green-400">
        <div className="hidden lg:block">
          <Navbar />
        </div>
        <div className="block lg:hidden">
          <MobileNav />
        </div>
        {children}
        <Footer />
      </main>
    );
  };
  
  export default Layout;