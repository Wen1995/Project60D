# Generated by the protocol buffer compiler.  DO NOT EDIT!
# source: proto/attrname_bytes.proto

import sys
_b=sys.version_info[0]<3 and (lambda x:x) or (lambda x:x.encode('latin1'))
from google.protobuf import descriptor as _descriptor
from google.protobuf import message as _message
from google.protobuf import reflection as _reflection
from google.protobuf import symbol_database as _symbol_database
from google.protobuf import descriptor_pb2
# @@protoc_insertion_point(imports)

_sym_db = _symbol_database.Default()




DESCRIPTOR = _descriptor.FileDescriptor(
  name='proto/attrname_bytes.proto',
  package='com.game.framework.resource.data',
  serialized_pb=_b('\n\x1aproto/attrname_bytes.proto\x12 com.game.framework.resource.data\"-\n\x08\x41TTRNAME\x12\x0c\n\x02id\x18\x01 \x02(\t:\x00\x12\x13\n\tATTR_name\x18\x02 \x02(\t:\x00\"K\n\x0e\x41TTRNAME_ARRAY\x12\x39\n\x05items\x18\x01 \x03(\x0b\x32*.com.game.framework.resource.data.ATTRNAME')
)
_sym_db.RegisterFileDescriptor(DESCRIPTOR)




_ATTRNAME = _descriptor.Descriptor(
  name='ATTRNAME',
  full_name='com.game.framework.resource.data.ATTRNAME',
  filename=None,
  file=DESCRIPTOR,
  containing_type=None,
  fields=[
    _descriptor.FieldDescriptor(
      name='id', full_name='com.game.framework.resource.data.ATTRNAME.id', index=0,
      number=1, type=9, cpp_type=9, label=2,
      has_default_value=True, default_value=_b("").decode('utf-8'),
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      options=None),
    _descriptor.FieldDescriptor(
      name='ATTR_name', full_name='com.game.framework.resource.data.ATTRNAME.ATTR_name', index=1,
      number=2, type=9, cpp_type=9, label=2,
      has_default_value=True, default_value=_b("").decode('utf-8'),
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      options=None),
  ],
  extensions=[
  ],
  nested_types=[],
  enum_types=[
  ],
  options=None,
  is_extendable=False,
  extension_ranges=[],
  oneofs=[
  ],
  serialized_start=64,
  serialized_end=109,
)


_ATTRNAME_ARRAY = _descriptor.Descriptor(
  name='ATTRNAME_ARRAY',
  full_name='com.game.framework.resource.data.ATTRNAME_ARRAY',
  filename=None,
  file=DESCRIPTOR,
  containing_type=None,
  fields=[
    _descriptor.FieldDescriptor(
      name='items', full_name='com.game.framework.resource.data.ATTRNAME_ARRAY.items', index=0,
      number=1, type=11, cpp_type=10, label=3,
      has_default_value=False, default_value=[],
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      options=None),
  ],
  extensions=[
  ],
  nested_types=[],
  enum_types=[
  ],
  options=None,
  is_extendable=False,
  extension_ranges=[],
  oneofs=[
  ],
  serialized_start=111,
  serialized_end=186,
)

_ATTRNAME_ARRAY.fields_by_name['items'].message_type = _ATTRNAME
DESCRIPTOR.message_types_by_name['ATTRNAME'] = _ATTRNAME
DESCRIPTOR.message_types_by_name['ATTRNAME_ARRAY'] = _ATTRNAME_ARRAY

ATTRNAME = _reflection.GeneratedProtocolMessageType('ATTRNAME', (_message.Message,), dict(
  DESCRIPTOR = _ATTRNAME,
  __module__ = 'proto.attrname_bytes_pb2'
  # @@protoc_insertion_point(class_scope:com.game.framework.resource.data.ATTRNAME)
  ))
_sym_db.RegisterMessage(ATTRNAME)

ATTRNAME_ARRAY = _reflection.GeneratedProtocolMessageType('ATTRNAME_ARRAY', (_message.Message,), dict(
  DESCRIPTOR = _ATTRNAME_ARRAY,
  __module__ = 'proto.attrname_bytes_pb2'
  # @@protoc_insertion_point(class_scope:com.game.framework.resource.data.ATTRNAME_ARRAY)
  ))
_sym_db.RegisterMessage(ATTRNAME_ARRAY)


# @@protoc_insertion_point(module_scope)