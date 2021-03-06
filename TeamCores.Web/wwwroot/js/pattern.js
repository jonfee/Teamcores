/*
   正则表达式
*/
const pattern = {
	"Text.User.Phone": /^((\+?86)|(\(\+86\)))?\d{3,4}-\d{7,8}(-\d{3,4})?$|^((\+?86)|(\(\+86\)))?1[34578]\d{9}$/,
	"Text.User.Email": /^([0-9a-zA-Z]+[-._+&])*[0-9a-zA-Z]+@([-0-9a-zA-Z]+[.])+[a-zA-Z]{2,6}$/,
	"Text.Integer": /^\d+$/,
	"Text.Integer.Positive": /^[1-9]\d*$/,
}