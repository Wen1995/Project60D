# Generated by the protocol buffer compiler.  DO NOT EDIT!
# source: proto/mucaijiagong_bytes.proto

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
  name='proto/mucaijiagong_bytes.proto',
  package='com.nkm.framework.resource.data',
  serialized_pb=_b('\n\x1eproto/mucaijiagong_bytes.proto\x12\x1f\x63om.nkm.framework.resource.data\"W\n\x0cMUCAIJIAGONG\x12\r\n\x02id\x18\x01 \x02(\x05:\x01\x30\x12\x1b\n\x10mucaijiagong_spd\x18\x02 \x01(\x05:\x01\x30\x12\x1b\n\x10mucaijiagong_cap\x18\x03 \x01(\x05:\x01\x30\"R\n\x12MUCAIJIAGONG_ARRAY\x12<\n\x05items\x18\x01 \x03(\x0b\x32-.com.nkm.framework.resource.data.MUCAIJIAGONG')
)
_sym_db.RegisterFileDescriptor(DESCRIPTOR)




_MUCAIJIAGONG = _descriptor.Descriptor(
  name='MUCAIJIAGONG',
  full_name='com.nkm.framework.resource.data.MUCAIJIAGONG',
  filename=None,
  file=DESCRIPTOR,
  containing_type=None,
  fields=[
    _descriptor.FieldDescriptor(
      name='id', full_name='com.nkm.framework.resource.data.MUCAIJIAGONG.id', index=0,
      number=1, type=5, cpp_type=1, label=2,
      has_default_value=True, default_value=0,
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      options=None),
    _descriptor.FieldDescriptor(
      name='mucaijiagong_spd', full_name='com.nkm.framework.resource.data.MUCAIJIAGONG.mucaijiagong_spd', index=1,
      number=2, type=5, cpp_type=1, label=1,
      has_default_value=True, default_value=0,
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      options=None),
    _descriptor.FieldDescriptor(
      name='mucaijiagong_cap', full_name='com.nkm.framework.resource.data.MUCAIJIAGONG.mucaijiagong_cap', index=2,
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
  serialized_start=67,
  serialized_end=154,
)


_MUCAIJIAGONG_ARRAY = _descriptor.Descriptor(
  name='MUCAIJIAGONG_ARRAY',
  full_name='com.nkm.framework.resource.data.MUCAIJIAGONG_ARRAY',
  filename=None,
  file=DESCRIPTOR,
  containing_type=None,
  fields=[
    _descriptor.FieldDescriptor(
      name='items', full_name='com.nkm.framework.resource.data.MUCAIJIAGONG_ARRAY.items', index=0,
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
  serialized_start=156,
  serialized_end=238,
)

_MUCAIJIAGONG_ARRAY.fields_by_name['items'].message_type = _MUCAIJIAGONG
DESCRIPTOR.message_types_by_name['MUCAIJIAGONG'] = _MUCAIJIAGONG
DESCRIPTOR.message_types_by_name['MUCAIJIAGONG_ARRAY'] = _MUCAIJIAGONG_ARRAY

MUCAIJIAGONG = _reflection.GeneratedProtocolMessageType('MUCAIJIAGONG', (_message.Message,), dict(
  DESCRIPTOR = _MUCAIJIAGONG,
  __module__ = 'proto.mucaijiagong_bytes_pb2'
  # @@protoc_insertion_point(class_scope:com.nkm.framework.resource.data.MUCAIJIAGONG)
  ))
_sym_db.RegisterMessage(MUCAIJIAGONG)

MUCAIJIAGONG_ARRAY = _reflection.GeneratedProtocolMessageType('MUCAIJIAGONG_ARRAY', (_message.Message,), dict(
  DESCRIPTOR = _MUCAIJIAGONG_ARRAY,
  __module__ = 'proto.mucaijiagong_bytes_pb2'
  # @@protoc_insertion_point(class_scope:com.nkm.framework.resource.data.MUCAIJIAGONG_ARRAY)
  ))
_sym_db.RegisterMessage(MUCAIJIAGONG_ARRAY)


# @@protoc_insertion_point(module_scope)
