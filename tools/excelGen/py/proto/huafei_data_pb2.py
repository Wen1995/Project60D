# Generated by the protocol buffer compiler.  DO NOT EDIT!
# source: proto/huafei_data.proto

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
  name='proto/huafei_data.proto',
  package='com.game.framework.resource.data',
  serialized_pb=_b('\n\x17proto/huafei_data.proto\x12 com.game.framework.resource.data\"E\n\x06HUAFEI\x12\r\n\x02id\x18\x01 \x02(\x05:\x01\x30\x12\x15\n\nhuafei_spd\x18\x02 \x01(\x05:\x01\x30\x12\x15\n\nhuafei_cap\x18\x03 \x01(\x05:\x01\x30\"G\n\x0cHUAFEI_ARRAY\x12\x37\n\x05items\x18\x01 \x03(\x0b\x32(.com.game.framework.resource.data.HUAFEI')
)
_sym_db.RegisterFileDescriptor(DESCRIPTOR)




_HUAFEI = _descriptor.Descriptor(
  name='HUAFEI',
  full_name='com.game.framework.resource.data.HUAFEI',
  filename=None,
  file=DESCRIPTOR,
  containing_type=None,
  fields=[
    _descriptor.FieldDescriptor(
      name='id', full_name='com.game.framework.resource.data.HUAFEI.id', index=0,
      number=1, type=5, cpp_type=1, label=2,
      has_default_value=True, default_value=0,
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      options=None),
    _descriptor.FieldDescriptor(
      name='huafei_spd', full_name='com.game.framework.resource.data.HUAFEI.huafei_spd', index=1,
      number=2, type=5, cpp_type=1, label=1,
      has_default_value=True, default_value=0,
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      options=None),
    _descriptor.FieldDescriptor(
      name='huafei_cap', full_name='com.game.framework.resource.data.HUAFEI.huafei_cap', index=2,
      number=3, type=5, cpp_type=1, label=1,
      has_default_value=True, default_value=0,
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
  serialized_start=61,
  serialized_end=130,
)


_HUAFEI_ARRAY = _descriptor.Descriptor(
  name='HUAFEI_ARRAY',
  full_name='com.game.framework.resource.data.HUAFEI_ARRAY',
  filename=None,
  file=DESCRIPTOR,
  containing_type=None,
  fields=[
    _descriptor.FieldDescriptor(
      name='items', full_name='com.game.framework.resource.data.HUAFEI_ARRAY.items', index=0,
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
  serialized_start=132,
  serialized_end=203,
)

_HUAFEI_ARRAY.fields_by_name['items'].message_type = _HUAFEI
DESCRIPTOR.message_types_by_name['HUAFEI'] = _HUAFEI
DESCRIPTOR.message_types_by_name['HUAFEI_ARRAY'] = _HUAFEI_ARRAY

HUAFEI = _reflection.GeneratedProtocolMessageType('HUAFEI', (_message.Message,), dict(
  DESCRIPTOR = _HUAFEI,
  __module__ = 'proto.huafei_data_pb2'
  # @@protoc_insertion_point(class_scope:com.game.framework.resource.data.HUAFEI)
  ))
_sym_db.RegisterMessage(HUAFEI)

HUAFEI_ARRAY = _reflection.GeneratedProtocolMessageType('HUAFEI_ARRAY', (_message.Message,), dict(
  DESCRIPTOR = _HUAFEI_ARRAY,
  __module__ = 'proto.huafei_data_pb2'
  # @@protoc_insertion_point(class_scope:com.game.framework.resource.data.HUAFEI_ARRAY)
  ))
_sym_db.RegisterMessage(HUAFEI_ARRAY)


# @@protoc_insertion_point(module_scope)
