import { Outlet } from 'react-router-dom';
import { useState } from 'react';
import Sidebar from '../components/Sidebar';
import Topbar from '../components/Topbar';
const Layout = () => {
  const [isSidebar, setIsSidebar] = useState(true);
  return (
    <>
      <Sidebar isSidebar={isSidebar}></Sidebar>
      <main className="content">
        <Topbar setIsSidebar={setIsSidebar} />
        <Outlet />
      </main>
    </>
  );
};

export default Layout;
