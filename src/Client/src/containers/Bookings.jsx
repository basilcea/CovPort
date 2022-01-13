import React from "react";
import Booking from "../components/Booking";
import { useHistory } from "react-router";

export default function Bookings({data}){
    const history = useHistory()
    const handleClick = () =>{ 
        history.push(`/createbooking`);
      }
    return(
        <div>
            <h3>Bookings</h3>
            {data && data.map(booking => {
                return <Booking key={booking.id} data={booking}/>
            })  
            }
            {data.length <1 && <p>You have no pending booking</p>}
            <button onClick={handleClick}>Create Booking</button>
        </div>
    )
}