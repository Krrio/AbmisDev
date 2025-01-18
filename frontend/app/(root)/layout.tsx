import Navbar from "@/components/Navbar";

const Layout = async ({ children }: { children: React.ReactNode }) => {

    return (
      <main className="container mx-auto border border-green-500 px-[80px]">
        <Navbar />
        {children}
      </main>
    );
  };
  
  export default Layout;