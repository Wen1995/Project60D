# Generated by the protocol buffer compiler.  DO NOT EDIT!
# source: proto/cangku_bytes.proto

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
  name='proto/cangku_bytes.proto',
  package='com.nkm.framework.resource.data',
  serialized_pb=_b('\n\x18proto/cangku_bytes.proto\x12\x1f\x63om.nkm.framework.resource.data\".\n\x06\x43\x41NGKU\x12\r\n\x02id\x18\x01 \x02(\x05:\x01\x30\x12\x15\n\ncangku_cap\x18\x02 \x01(\x05:\x01\x30\"F\n\x0c\x43\x41NGKU_ARRAY\x12\x36\n\x05items\x18\x01 \x03(\x0b\x32\'.com.nkm.framework.resource.data.CANGKU')
)
_sym_db.RegisterFileDescriptor(DESCRIPTOR)




_CANGKU = _descriptor.Descriptor(
  name='CANGKU',
  full_name='com.nkm.framework.resource.data.CANGKU',
  filename=None,
  file=DESCRIPTOR,
  containing_type=None,
  fields=[
    _descriptor.FieldDescriptor(
      name='id', full_name='com.nkm.framework.resource.data.CANGKU.id', index=0,
      number=1, type=5, cpp_type=1, label=2,
      has_default_value=True, default_value=0,
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      options=None),
    _descriptor.FieldDescriptor(
      name='cangku_cap', full_name='com.nkm.framework.resource.data.CANGKU.cangku_cap', index=1,
      number=2, type=5, cpp_type=1, label=1,
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
  serialized_end=107,
)


_CANGKU_ARRAY = _descriptor.Descriptor(
  name='CANGKU_ARRAY',
  full_name='com.nkm.framework.resource.data.CANGKU_ARRAY',
  filename=None,
  file=DESCRIPTOR,
  containing_type=None,
  fields=[
    _descriptor.FieldDescriptor(
      name='items', full_name='com.nkm.framework.resource.data.CANGKU_ARRAY.items', index=0,
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
  serialized_start=109,
  serialized_end=179,
)

_CANGKU_ARRAY.fields_by_name['items'].message_type = _CANGKU
DESCRIPTOR.message_types_by_name['CANGKU'] = _CANGKU
DESCRIPTOR.message_types_by_name['CANGKU_ARRAY'] = _CANGKU_ARRAY

CANGKU = _reflection.GeneratedProtocolMessageType('CANGKU', (_message.Message,), dict(
  DESCRIPTOR = _CANGKU,
  __module__ = 'proto.cangku_bytes_pb2'
  # @@protoc_insertion_point(class_scope:com.nkm.framework.resource.data.CANGKU)
  ))
_sym_db.RegisterMessage(CANGKU)

CANGKU_ARRAY = _reflection.GeneratedProtocolMessageType('CANGKU_ARRAY', (_message.Message,), dict(
  DESCRIPTOR = _CANGKU_ARRAY,
  __module__ = 'proto.cangku_bytes_pb2'
  # @@protoc_insertion_point(class_scope:com.nkm.framework.resource.data.CANGKU_ARRAY)
  ))
_sym_db.RegisterMessage(CANGKU_ARRAY)


# @@protoc_insertion_point(module_scope)
