import React, { useState, useEffect } from "react";
import axios from "axios";
import User from "../components/User";
import Booking from "./Bookings";
import Tests from "./Tests";
import { useHistory } from "react-router";
import HandleError from "../Helpers/handleError";

export default function Home() {
  const [detail, setDetail] = useState({ email: "" });
  const [user, setUser] = useState(false);
  const [isLoaded, setLoaded] = useState(false);
  const [userSummary, setUserSummary] = useState({
    user: {},
    bookings: [],
    tests: [],
  });
  const history = useHistory();
  useEffect(() => {
    const getUser = async () => {
      await axios.get(`/space`);
      let userId = localStorage.getItem("userId");
      if (userId) {
        await getUserSummary(userId);
      }
      setLoaded(true);
    };
    try {
      getUser();
    } catch (error) {
      HandleError(error, setErrors);
    }
  }, [isLoaded]);
  const handleClick = () => {
    history.push(`/createspace`);
  };
  const [errors, setErrors] = useState([]);

  const getUserSummary = async (id) => {
    const {
      data: { data },
    } = await axios.get(`/user/summary/${id}`);
    setUser(true);
    setUserSummary((prev) => ({
      ...prev,
      bookings: data.bookings,
      user: data.user,
      tests: data.tests,
    }));
  };
  const refreshUser = () => {
    localStorage.removeItem("userId");
    window.location.reload();
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    try {
      const result = await axios.get(`/user?email=${detail.email}`);
      if (result && result.data) {
        const userinfo = result.data.data[0];
        await getUserSummary(userinfo.id);
        localStorage.setItem("userId", userinfo.id);
      }
    } catch (error) {
      HandleError(error, setErrors);
    }
    setDetail((prev) => ({ ...prev, email: "" }));
  };
  const change = (e) => {
    const target = e.target;
    const value = target.value;
    const name = target.name;
    setDetail((prev) => ({ ...prev, [name]: value }));
  };
  return (
    <div>
      <h1>Stay Healthy, Fight Covid, Get Tested!</h1>

      {isLoaded && !user && (
        <div>
          <p> To begin, Enter your email address below </p>
          <form onSubmit={handleSubmit}>
            <input
              type="email"
              value={detail.email}
              name="email"
              placeholder="Your Email"
              onChange={change}
              required
            />
            <input type="submit" value="Enter" />
          </form>
        </div>
      )}

      {isLoaded && user && (
        <div>
          <User data={userSummary.user} />
          {userSummary.user.userRole === "ADMIN" && (
            <button onClick={handleClick}> Create Space </button>
          )}
          <Booking data={userSummary.bookings} />
          <Tests data={userSummary.tests} role={userSummary.user.userRole} />
        </div>
      )}
      {errors &&
        errors.map((error, i) => {
          return <p key={`error-${i}`}>{error}</p>;
        })}

      {user && <button onClick={refreshUser}>Clear Data</button>}
    </div>
  );
}
