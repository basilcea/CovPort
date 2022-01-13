import React, { useState } from "react";
import Calendar from "react-calendar";
import "react-calendar/dist/Calendar.css";
import axios from "axios";
import handleError from "../Helpers/handleError";
import { NavLink } from "react-router-dom";

export default function CreateSpace() {
  const [result, setResult] = useState();
  const [errors, setErrors] = useState([]);
  const [space, setSpace] = useState({
    locationName: "",
    date: new Date(),
    spacesCreated: 0,
    requesterId: localStorage.getItem("userId"),
  });
  const onChange = (e) => {
    setSpace((prev) => ({
      ...prev,
      date: e,
    }));
  };
  const change = (e) => {
    const target = e.target;
    const value = target.value;
    const name = target.name;
    setSpace((prev) => ({ ...prev, [name]: value }));
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    space.date = space.date.toISOString().split("T")[0];
    try {
      const {
        data: { data },
      } = await axios.post(`/space`, space);
      onChange(new Date(data.date));
      setResult(data);
    } catch (error) {
      handleError(error, setErrors);
    }
  };

  return (
    <div>
      <h2> Create Space</h2>
      <form onSubmit={handleSubmit}>
        <input
          type="text"
          value={space.email}
          name="locationName"
          placeholder="Name of Location"
          onChange={change}
          required
        />

        <input
          type="number"
          value={space.spaces}
          name="spacesCreated"
          placeholder="Number Of Spaces"
          onChange={change}
          required
        />

        <Calendar
          onChange={(e) => onChange(e)}
          value={space.date}
          name="date"
          required
        />
        <input type="submit" value="Enter" />
      </form>

      {result && (
        <div style={{ marginTop: "5px" }}>
          <b> Space created </b>
          <p> LocationName: {result.locationName}</p>
          <p>Date: {new Date(result.date).toDateString()}</p>
          <p>SpaceAvailable: {result.spacesAvailable}</p>
          <p>spaceCreated: {result.spacesCreated}</p>
        </div>
      )}
      {errors &&
        errors.map((error, i) => {
          return (
            <p style={{ marginTop: "5px" }} key={`error-${i}`}>
              {error}
            </p>
          );
        })}
      <NavLink to="/">View Booking</NavLink>
    </div>
  );
}
