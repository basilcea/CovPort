import React, { useState } from "react";
import "react-calendar/dist/Calendar.css";
import axios from "axios";
import HandleError from "../Helpers/handleError";
import { NavLink } from "react-router-dom";

export default function CreateBooking() {
  const [result, setResult] = useState();
  const [errors, setErrors] = useState([]);
  const [booking, setBooking] = useState({
    testType: "",
    spaceId: 0,
    userId: localStorage.getItem("userId"),
  });
  const [location, setLocation] = useState({ value: "" });
  const [spaces, setSpaces] = useState([]);

  const change = (e) => {
    const target = e.target;
    const value = target.value;
    const name = target.name;
    setBooking((prev) => ({ ...prev, [name]: value }));
  };

  const setLocationVal = (e) => {
    setErrors([]);
    const target = e.target;
    const value = target.value;
    const name = target.name;
    setLocation((prev) => ({ ...prev, [name]: value }));
    e.preventDefault();
  };

  const getSpaces = async (e) => {
    setErrors([]);
    e.preventDefault();
    try {
      const {
        data: { data },
      } = await axios.get(`/space?location=${location.value}`);
      setSpaces(data);
    } catch (error) {
      HandleError(error, setErrors);
    }
  };

  const handleSubmit = async (e) => {
    setErrors([]);
    e.preventDefault();
    try {
      const {
        data: { data },
      } = await axios.post(`/booking`, booking);
      setResult(data);
    } catch (error) {
      HandleError(error, setErrors);
      setSpaces([]);
    }
  };

  return (
    <div>
      <h2> Create Booking</h2>

      <form onSubmit={getSpaces}>
        <input
          type="text"
          value={location.value}
          name="value"
          placeholder="Name of Location"
          onChange={setLocationVal}
          required
        />
        <input type="submit" value="Enter" />
      </form>
      {spaces.length > 0 && !result && (
        <div>
          <h3>Book a test in {location.value}</h3>
          <form onSubmit={handleSubmit}>
            <label>
              Rapid Test
              <input
                type="radio"
                value={"Rapid"}
                name="testType"
                onChange={change}
              />
            </label>

            <label>
              PCR Test
              <input
                type="radio"
                value={"PCR"}
                name="testType"
                onChange={change}
                required
              />
            </label>
            <h4>Test Date</h4>
            {spaces.map((space) => (
              <label key={space.id}>
                {new Date(space.date).toDateString()}
                <input
                  type="radio"
                  value={space.id}
                  name="spaceId"
                  onChange={change}
                  required
                />
              </label>
            ))}
            <input type="submit" value="Enter" />
          </form>{" "}
        </div>
      )}
      {result && (
        <div style={{ marginTop: "5px" }}>
          <b> Booking Created </b>
          <NavLink to="/">View Booking</NavLink>
        </div>
      )}
      <div>
        {errors &&
          errors.map((error, i) => {
            return (
              <p style={{ marginTop: "5px" }} key={`error-${i}`}>
                {error}
              </p>
            );
          })}
      </div>
    </div>
  );
}
