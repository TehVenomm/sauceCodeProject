.class public interface abstract Lcom/helpshift/common/domain/network/NetworkErrorCodes;
.super Ljava/lang/Object;
.source "NetworkErrorCodes.java"


# static fields
.field public static final AUTH_TOKEN_NOT_PROVIDED:Ljava/lang/Integer;

.field public static final CONFLICT:Ljava/lang/Integer;

.field public static final CONTENT_NOT_FOUND:Ljava/lang/Integer;

.field public static final CONTENT_UNCHANGED:Ljava/lang/Integer;

.field public static final ENTITY_TOO_LARGE:Ljava/lang/Integer;

.field public static final EXPECTATION_FAILED:Ljava/lang/Integer;

.field public static final FAILED_DEPENDENCY:Ljava/lang/Integer;

.field public static final FORBIDDEN_ACCESS:Ljava/lang/Integer;

.field public static final GENERIC_NETWORK_ERROR:Ljava/lang/Integer;

.field public static final GONE_ERROR:Ljava/lang/Integer;

.field public static final INVALID_AUTH_TOKEN:Ljava/lang/Integer;

.field public static final LOCKED:Ljava/lang/Integer;

.field public static final METHOD_NOT_ALLOWED:Ljava/lang/Integer;

.field public static final NOT_RETRIABLE_STATUS_CODES:Ljava/util/Set;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/Set<",
            "Ljava/lang/Integer;",
            ">;"
        }
    .end annotation
.end field

.field public static final NO_CONNECTION:Ljava/lang/Integer;

.field public static final NO_REQUEST_LENGTH:Ljava/lang/Integer;

.field public static final OBJECT_NOT_FOUND:Ljava/lang/Integer;

.field public static final OK:Ljava/lang/Integer;

.field public static final PARSE_ERROR:Ljava/lang/Integer;

.field public static final PAYMENT_REQUIRED:Ljava/lang/Integer;

.field public static final PRECONDITION_REQUIRED:Ljava/lang/Integer;

.field public static final PRE_CONDITION_NOT_MATCHED:Ljava/lang/Integer;

.field public static final PROCESSING_REQUEST:Ljava/lang/Integer;

.field public static final RANGE_NOT_SATISFIABLE:Ljava/lang/Integer;

.field public static final REQUEST_HEADER_FIELDS_LARGE:Ljava/lang/Integer;

.field public static final REQUEST_TIMEOUT:Ljava/lang/Integer;

.field public static final SCREENSHOT_UPLOAD_ERROR:Ljava/lang/Integer;

.field public static final SERVER_ERROR:Ljava/lang/Integer;

.field public static final SSL_HANDSHAKE_ERROR:Ljava/lang/Integer;

.field public static final SSL_PEER_UNVERIFIED_ERROR:Ljava/lang/Integer;

.field public static final TIMESTAMP_MISMATCH:Ljava/lang/Integer;

.field public static final UNAUTHORIZED_ACCESS:Ljava/lang/Integer;

.field public static final UNAVAILABLE_LEGAL_REASONS:Ljava/lang/Integer;

.field public static final UNKNOWN_HOST_ERROR:Ljava/lang/Integer;

.field public static final UNSUPPORTED_MEDIA_TYPE:Ljava/lang/Integer;

.field public static final UPGRADE_REQUIRED:Ljava/lang/Integer;

.field public static final URI_TOO_LONG:Ljava/lang/Integer;


# direct methods
.method static constructor <clinit>()V
    .locals 9

    const/4 v0, 0x0

    .line 8
    invoke-static {v0}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v1

    sput-object v1, Lcom/helpshift/common/domain/network/NetworkErrorCodes;->NO_CONNECTION:Ljava/lang/Integer;

    const/4 v1, 0x1

    .line 9
    invoke-static {v1}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v2

    sput-object v2, Lcom/helpshift/common/domain/network/NetworkErrorCodes;->GENERIC_NETWORK_ERROR:Ljava/lang/Integer;

    const/4 v2, 0x2

    .line 10
    invoke-static {v2}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v3

    sput-object v3, Lcom/helpshift/common/domain/network/NetworkErrorCodes;->SCREENSHOT_UPLOAD_ERROR:Ljava/lang/Integer;

    const/4 v3, 0x3

    .line 11
    invoke-static {v3}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v4

    sput-object v4, Lcom/helpshift/common/domain/network/NetworkErrorCodes;->UNKNOWN_HOST_ERROR:Ljava/lang/Integer;

    const/4 v4, 0x4

    .line 12
    invoke-static {v4}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v5

    sput-object v5, Lcom/helpshift/common/domain/network/NetworkErrorCodes;->SSL_PEER_UNVERIFIED_ERROR:Ljava/lang/Integer;

    const/4 v5, 0x5

    .line 13
    invoke-static {v5}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v6

    sput-object v6, Lcom/helpshift/common/domain/network/NetworkErrorCodes;->SSL_HANDSHAKE_ERROR:Ljava/lang/Integer;

    const/16 v6, 0x66

    .line 14
    invoke-static {v6}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v6

    sput-object v6, Lcom/helpshift/common/domain/network/NetworkErrorCodes;->PROCESSING_REQUEST:Ljava/lang/Integer;

    const/16 v6, 0xc8

    .line 15
    invoke-static {v6}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v6

    sput-object v6, Lcom/helpshift/common/domain/network/NetworkErrorCodes;->OK:Ljava/lang/Integer;

    const/16 v6, 0x130

    .line 16
    invoke-static {v6}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v6

    sput-object v6, Lcom/helpshift/common/domain/network/NetworkErrorCodes;->CONTENT_UNCHANGED:Ljava/lang/Integer;

    const/16 v6, 0x190

    .line 17
    invoke-static {v6}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v6

    sput-object v6, Lcom/helpshift/common/domain/network/NetworkErrorCodes;->OBJECT_NOT_FOUND:Ljava/lang/Integer;

    const/16 v6, 0x191

    .line 18
    invoke-static {v6}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v6

    sput-object v6, Lcom/helpshift/common/domain/network/NetworkErrorCodes;->UNAUTHORIZED_ACCESS:Ljava/lang/Integer;

    const/16 v6, 0x192

    .line 19
    invoke-static {v6}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v6

    sput-object v6, Lcom/helpshift/common/domain/network/NetworkErrorCodes;->PAYMENT_REQUIRED:Ljava/lang/Integer;

    const/16 v6, 0x193

    .line 20
    invoke-static {v6}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v6

    sput-object v6, Lcom/helpshift/common/domain/network/NetworkErrorCodes;->FORBIDDEN_ACCESS:Ljava/lang/Integer;

    const/16 v6, 0x194

    .line 21
    invoke-static {v6}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v6

    sput-object v6, Lcom/helpshift/common/domain/network/NetworkErrorCodes;->CONTENT_NOT_FOUND:Ljava/lang/Integer;

    const/16 v6, 0x195

    .line 22
    invoke-static {v6}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v6

    sput-object v6, Lcom/helpshift/common/domain/network/NetworkErrorCodes;->METHOD_NOT_ALLOWED:Ljava/lang/Integer;

    const/16 v6, 0x196

    .line 23
    invoke-static {v6}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v6

    sput-object v6, Lcom/helpshift/common/domain/network/NetworkErrorCodes;->PARSE_ERROR:Ljava/lang/Integer;

    const/16 v6, 0x198

    .line 24
    invoke-static {v6}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v6

    sput-object v6, Lcom/helpshift/common/domain/network/NetworkErrorCodes;->REQUEST_TIMEOUT:Ljava/lang/Integer;

    const/16 v6, 0x199

    .line 25
    invoke-static {v6}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v6

    sput-object v6, Lcom/helpshift/common/domain/network/NetworkErrorCodes;->CONFLICT:Ljava/lang/Integer;

    const/16 v6, 0x19a

    .line 26
    invoke-static {v6}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v6

    sput-object v6, Lcom/helpshift/common/domain/network/NetworkErrorCodes;->GONE_ERROR:Ljava/lang/Integer;

    const/16 v6, 0x19b

    .line 27
    invoke-static {v6}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v6

    sput-object v6, Lcom/helpshift/common/domain/network/NetworkErrorCodes;->NO_REQUEST_LENGTH:Ljava/lang/Integer;

    const/16 v6, 0x19c

    .line 28
    invoke-static {v6}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v6

    sput-object v6, Lcom/helpshift/common/domain/network/NetworkErrorCodes;->PRE_CONDITION_NOT_MATCHED:Ljava/lang/Integer;

    const/16 v6, 0x19d

    .line 29
    invoke-static {v6}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v6

    sput-object v6, Lcom/helpshift/common/domain/network/NetworkErrorCodes;->ENTITY_TOO_LARGE:Ljava/lang/Integer;

    const/16 v6, 0x19e

    .line 30
    invoke-static {v6}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v6

    sput-object v6, Lcom/helpshift/common/domain/network/NetworkErrorCodes;->URI_TOO_LONG:Ljava/lang/Integer;

    const/16 v6, 0x19f

    .line 31
    invoke-static {v6}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v6

    sput-object v6, Lcom/helpshift/common/domain/network/NetworkErrorCodes;->UNSUPPORTED_MEDIA_TYPE:Ljava/lang/Integer;

    const/16 v6, 0x1a0

    .line 32
    invoke-static {v6}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v6

    sput-object v6, Lcom/helpshift/common/domain/network/NetworkErrorCodes;->RANGE_NOT_SATISFIABLE:Ljava/lang/Integer;

    const/16 v6, 0x1a1

    .line 33
    invoke-static {v6}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v6

    sput-object v6, Lcom/helpshift/common/domain/network/NetworkErrorCodes;->EXPECTATION_FAILED:Ljava/lang/Integer;

    const/16 v6, 0x1a6

    .line 34
    invoke-static {v6}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v6

    sput-object v6, Lcom/helpshift/common/domain/network/NetworkErrorCodes;->TIMESTAMP_MISMATCH:Ljava/lang/Integer;

    const/16 v6, 0x1a7

    .line 35
    invoke-static {v6}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v6

    sput-object v6, Lcom/helpshift/common/domain/network/NetworkErrorCodes;->LOCKED:Ljava/lang/Integer;

    const/16 v6, 0x1a8

    .line 36
    invoke-static {v6}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v6

    sput-object v6, Lcom/helpshift/common/domain/network/NetworkErrorCodes;->FAILED_DEPENDENCY:Ljava/lang/Integer;

    const/16 v6, 0x1aa

    .line 37
    invoke-static {v6}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v6

    sput-object v6, Lcom/helpshift/common/domain/network/NetworkErrorCodes;->UPGRADE_REQUIRED:Ljava/lang/Integer;

    const/16 v6, 0x1ac

    .line 38
    invoke-static {v6}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v6

    sput-object v6, Lcom/helpshift/common/domain/network/NetworkErrorCodes;->PRECONDITION_REQUIRED:Ljava/lang/Integer;

    const/16 v6, 0x1af

    .line 39
    invoke-static {v6}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v6

    sput-object v6, Lcom/helpshift/common/domain/network/NetworkErrorCodes;->REQUEST_HEADER_FIELDS_LARGE:Ljava/lang/Integer;

    const/16 v6, 0x1b9

    .line 43
    invoke-static {v6}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v6

    sput-object v6, Lcom/helpshift/common/domain/network/NetworkErrorCodes;->AUTH_TOKEN_NOT_PROVIDED:Ljava/lang/Integer;

    const/16 v6, 0x1bb

    .line 44
    invoke-static {v6}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v6

    sput-object v6, Lcom/helpshift/common/domain/network/NetworkErrorCodes;->INVALID_AUTH_TOKEN:Ljava/lang/Integer;

    const/16 v6, 0x1c3

    .line 45
    invoke-static {v6}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v6

    sput-object v6, Lcom/helpshift/common/domain/network/NetworkErrorCodes;->UNAVAILABLE_LEGAL_REASONS:Ljava/lang/Integer;

    const/16 v6, 0x1f4

    .line 47
    invoke-static {v6}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v6

    sput-object v6, Lcom/helpshift/common/domain/network/NetworkErrorCodes;->SERVER_ERROR:Ljava/lang/Integer;

    .line 48
    new-instance v6, Ljava/util/HashSet;

    const/16 v7, 0x15

    new-array v7, v7, [Ljava/lang/Integer;

    sget-object v8, Lcom/helpshift/common/domain/network/NetworkErrorCodes;->OBJECT_NOT_FOUND:Ljava/lang/Integer;

    aput-object v8, v7, v0

    sget-object v0, Lcom/helpshift/common/domain/network/NetworkErrorCodes;->PAYMENT_REQUIRED:Ljava/lang/Integer;

    aput-object v0, v7, v1

    sget-object v0, Lcom/helpshift/common/domain/network/NetworkErrorCodes;->FORBIDDEN_ACCESS:Ljava/lang/Integer;

    aput-object v0, v7, v2

    sget-object v0, Lcom/helpshift/common/domain/network/NetworkErrorCodes;->CONTENT_NOT_FOUND:Ljava/lang/Integer;

    aput-object v0, v7, v3

    sget-object v0, Lcom/helpshift/common/domain/network/NetworkErrorCodes;->METHOD_NOT_ALLOWED:Ljava/lang/Integer;

    aput-object v0, v7, v4

    sget-object v0, Lcom/helpshift/common/domain/network/NetworkErrorCodes;->PARSE_ERROR:Ljava/lang/Integer;

    aput-object v0, v7, v5

    sget-object v0, Lcom/helpshift/common/domain/network/NetworkErrorCodes;->CONFLICT:Ljava/lang/Integer;

    const/4 v1, 0x6

    aput-object v0, v7, v1

    sget-object v0, Lcom/helpshift/common/domain/network/NetworkErrorCodes;->GONE_ERROR:Ljava/lang/Integer;

    const/4 v1, 0x7

    aput-object v0, v7, v1

    sget-object v0, Lcom/helpshift/common/domain/network/NetworkErrorCodes;->NO_REQUEST_LENGTH:Ljava/lang/Integer;

    const/16 v1, 0x8

    aput-object v0, v7, v1

    sget-object v0, Lcom/helpshift/common/domain/network/NetworkErrorCodes;->PRE_CONDITION_NOT_MATCHED:Ljava/lang/Integer;

    const/16 v1, 0x9

    aput-object v0, v7, v1

    sget-object v0, Lcom/helpshift/common/domain/network/NetworkErrorCodes;->ENTITY_TOO_LARGE:Ljava/lang/Integer;

    const/16 v1, 0xa

    aput-object v0, v7, v1

    sget-object v0, Lcom/helpshift/common/domain/network/NetworkErrorCodes;->URI_TOO_LONG:Ljava/lang/Integer;

    const/16 v1, 0xb

    aput-object v0, v7, v1

    sget-object v0, Lcom/helpshift/common/domain/network/NetworkErrorCodes;->UNSUPPORTED_MEDIA_TYPE:Ljava/lang/Integer;

    const/16 v1, 0xc

    aput-object v0, v7, v1

    sget-object v0, Lcom/helpshift/common/domain/network/NetworkErrorCodes;->RANGE_NOT_SATISFIABLE:Ljava/lang/Integer;

    const/16 v1, 0xd

    aput-object v0, v7, v1

    sget-object v0, Lcom/helpshift/common/domain/network/NetworkErrorCodes;->EXPECTATION_FAILED:Ljava/lang/Integer;

    const/16 v1, 0xe

    aput-object v0, v7, v1

    sget-object v0, Lcom/helpshift/common/domain/network/NetworkErrorCodes;->LOCKED:Ljava/lang/Integer;

    const/16 v1, 0xf

    aput-object v0, v7, v1

    sget-object v0, Lcom/helpshift/common/domain/network/NetworkErrorCodes;->FAILED_DEPENDENCY:Ljava/lang/Integer;

    const/16 v1, 0x10

    aput-object v0, v7, v1

    sget-object v0, Lcom/helpshift/common/domain/network/NetworkErrorCodes;->UPGRADE_REQUIRED:Ljava/lang/Integer;

    const/16 v1, 0x11

    aput-object v0, v7, v1

    sget-object v0, Lcom/helpshift/common/domain/network/NetworkErrorCodes;->PRECONDITION_REQUIRED:Ljava/lang/Integer;

    const/16 v1, 0x12

    aput-object v0, v7, v1

    sget-object v0, Lcom/helpshift/common/domain/network/NetworkErrorCodes;->REQUEST_HEADER_FIELDS_LARGE:Ljava/lang/Integer;

    const/16 v1, 0x13

    aput-object v0, v7, v1

    sget-object v0, Lcom/helpshift/common/domain/network/NetworkErrorCodes;->UNAVAILABLE_LEGAL_REASONS:Ljava/lang/Integer;

    const/16 v1, 0x14

    aput-object v0, v7, v1

    invoke-static {v7}, Ljava/util/Arrays;->asList([Ljava/lang/Object;)Ljava/util/List;

    move-result-object v0

    invoke-direct {v6, v0}, Ljava/util/HashSet;-><init>(Ljava/util/Collection;)V

    sput-object v6, Lcom/helpshift/common/domain/network/NetworkErrorCodes;->NOT_RETRIABLE_STATUS_CODES:Ljava/util/Set;

    return-void
.end method
