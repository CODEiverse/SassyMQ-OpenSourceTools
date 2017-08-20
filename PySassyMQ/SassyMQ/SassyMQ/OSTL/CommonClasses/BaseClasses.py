import json
import datetime
import time
import copy
import uuid

def json_default(value):
    if isinstance(value, datetime.date) or isinstance(value, datetime.datetime):
        return time.strftime('%Y-%m-%d %H:%M:%S', value.timetuple())
    elif isinstance(value, time.struct_time):
        return time.strftime('%Y-%m-%d %H:%M:%S', value)
    elif isinstance(value, uuid.UUID):
        return str(value)
    elif hasattr(value, "__dict__"):
        copyOfDict = copy.copy(value.__dict__)
        for key in copyOfDict.keys():
            dictVal = copyOfDict[key]
            if isinstance(dictVal, datetime.date) or isinstance(dictVal, datetime.datetime):
                dictVal = time.strftime('%Y-%m-%d %H:%M:%S', dictVal.timetuple())
            elif isinstance(dictVal, time.struct_time):
                dictVal = time.strftime('%Y-%m-%d %H:%M:%S', dictVal)
            elif isinstance(dictVal, uuid.UUID):
                dictVal = str(dictVal)
            elif isinstance(dictVal, Object):
                dictVal = json_default(dictVal)
            copyOfDict[key] = dictVal
        return copyOfDict
    else: return ""


class Object(object):
    def toJSON(self):
        objToSerialize = copy.copy(self);
        if hasattr(objToSerialize, 'Actor'): objToSerialize.Actor = None;
        return json.dumps(objToSerialize, default=json_default)

