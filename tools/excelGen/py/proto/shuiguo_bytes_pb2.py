# Generated by the protocol buffer compiler.  DO NOT EDIT!
# source: proto/shuiguo_bytes.proto

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
  name='proto/shuiguo_bytes.proto',
  package='com.nkm.framework.resource.data',
  serialized_pb=_b('\n\x19proto/shuiguo_bytes.proto\x12\x1f\x63om.nkm.framework.resource.data\"H\n\x07SHUIGUO\x12\r\n\x02id\x18\x01 \x02(\x05:\x01\x30\x12\x16\n\x0bshuiguo_spd\x18\x02 \x01(\x05:\x01\x30\x12\x16\n\x0bshuiguo_cap\x18\x03 \x01(\x05:\x01\x30\"H\n\rSHUIGUO_ARRAY\x12\x37\n\x05items\x18\x01 \x03(\x0b\x32(.com.nkm.framework.resource.data.SHUIGUO')
)
_sym_db.RegisterFileDescriptor(DESCRIPTOR)




_SHUIGUO = _descriptor.Descriptor(
  name='SHUIGUO',
  full_name='com.nkm.framework.resource.data.SHUIGUO',
  filename=None,
  file=DESCRIPTOR,
  containing_type=None,
  fields=[
    _descriptor.FieldDescriptor(
      name='id', full_name='com.nkm.framework.resource.data.SHUIGUO.id', index=0,
      number=1, type=5, cpp_type=1, label=2,
      has_default_value=True, default_value=0,
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      options=None),
    _descriptor.FieldDescriptor(
      name='shuiguo_spd', full_name='com.nkm.framework.resource.data.SHUIGUO.shuiguo_spd', index=1,
      number=2, type=5, cpp_type=1, label=1,
      has_default_value=True, default_value=0,
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      options=None),
    _descriptor.FieldDescriptor(
      name='shuiguo_cap', full_name='com.nkm.framework.resource.data.SHUIGUO.shuiguo_cap', index=2,
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
  serialized_start=62,
  serialized_end=134,
)


_SHUIGUO_ARRAY = _descriptor.Descriptor(
  name='SHUIGUO_ARRAY',
  full_name='com.nkm.framework.resource.data.SHUIGUO_ARRAY',
  filename=None,
  file=DESCRIPTOR,
  containing_type=None,
  fields=[
    _descriptor.FieldDescriptor(
      name='items', full_name='com.nkm.framework.resource.data.SHUIGUO_ARRAY.items', index=0,
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
  serialized_start=136,
  serialized_end=208,
)

_SHUIGUO_ARRAY.fields_by_name['items'].message_type = _SHUIGUO
DESCRIPTOR.message_types_by_name['SHUIGUO'] = _SHUIGUO
DESCRIPTOR.message_types_by_name['SHUIGUO_ARRAY'] = _SHUIGUO_ARRAY

SHUIGUO = _reflection.GeneratedProtocolMessageType('SHUIGUO', (_message.Message,), dict(
  DESCRIPTOR = _SHUIGUO,
  __module__ = 'proto.shuiguo_bytes_pb2'
  # @@protoc_insertion_point(class_scope:com.nkm.framework.resource.data.SHUIGUO)
  ))
_sym_db.RegisterMessage(SHUIGUO)

SHUIGUO_ARRAY = _reflection.GeneratedProtocolMessageType('SHUIGUO_ARRAY', (_message.Message,), dict(
  DESCRIPTOR = _SHUIGUO_ARRAY,
  __module__ = 'proto.shuiguo_bytes_pb2'
  # @@protoc_insertion_point(class_scope:com.nkm.framework.resource.data.SHUIGUO_ARRAY)
  ))
_sym_db.RegisterMessage(SHUIGUO_ARRAY)


# @@protoc_insertion_point(module_scope)
