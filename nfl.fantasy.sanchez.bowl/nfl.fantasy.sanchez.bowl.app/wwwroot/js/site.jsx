//var Comp = React.createClass({
//    render: function () {
//        return 
//            <div>
//                React app
//                </div>
//    }
//});
//ReactDOM.Render(
//    <Comp />,
//    document.getElementById('content')
//);
/** @jsx React.DOM */
const Comp = (prop) => {
    return (
        <div>React App (traditional not using asp react)</div>
    );
};

ReactDOM.render(<Comp/>, _mountNode);