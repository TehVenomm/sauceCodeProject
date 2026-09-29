.class public final enum Lcom/helpshift/common/platform/Device$PermissionType;
.super Ljava/lang/Enum;
.source "Device.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/helpshift/common/platform/Device;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x4019
    name = "PermissionType"
.end annotation

.annotation system Ldalvik/annotation/Signature;
    value = {
        "Ljava/lang/Enum<",
        "Lcom/helpshift/common/platform/Device$PermissionType;",
        ">;"
    }
.end annotation


# static fields
.field private static final synthetic $VALUES:[Lcom/helpshift/common/platform/Device$PermissionType;

.field public static final enum READ_STORAGE:Lcom/helpshift/common/platform/Device$PermissionType;

.field public static final enum WRITE_STORAGE:Lcom/helpshift/common/platform/Device$PermissionType;


# direct methods
.method static constructor <clinit>()V
    .locals 4

    .line 72
    new-instance v0, Lcom/helpshift/common/platform/Device$PermissionType;

    const-string v1, "READ_STORAGE"

    const/4 v2, 0x0

    invoke-direct {v0, v1, v2}, Lcom/helpshift/common/platform/Device$PermissionType;-><init>(Ljava/lang/String;I)V

    sput-object v0, Lcom/helpshift/common/platform/Device$PermissionType;->READ_STORAGE:Lcom/helpshift/common/platform/Device$PermissionType;

    .line 73
    new-instance v0, Lcom/helpshift/common/platform/Device$PermissionType;

    const-string v1, "WRITE_STORAGE"

    const/4 v3, 0x1

    invoke-direct {v0, v1, v3}, Lcom/helpshift/common/platform/Device$PermissionType;-><init>(Ljava/lang/String;I)V

    sput-object v0, Lcom/helpshift/common/platform/Device$PermissionType;->WRITE_STORAGE:Lcom/helpshift/common/platform/Device$PermissionType;

    const/4 v0, 0x2

    .line 71
    new-array v0, v0, [Lcom/helpshift/common/platform/Device$PermissionType;

    sget-object v1, Lcom/helpshift/common/platform/Device$PermissionType;->READ_STORAGE:Lcom/helpshift/common/platform/Device$PermissionType;

    aput-object v1, v0, v2

    sget-object v1, Lcom/helpshift/common/platform/Device$PermissionType;->WRITE_STORAGE:Lcom/helpshift/common/platform/Device$PermissionType;

    aput-object v1, v0, v3

    sput-object v0, Lcom/helpshift/common/platform/Device$PermissionType;->$VALUES:[Lcom/helpshift/common/platform/Device$PermissionType;

    return-void
.end method

.method private constructor <init>(Ljava/lang/String;I)V
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()V"
        }
    .end annotation

    .line 71
    invoke-direct {p0, p1, p2}, Ljava/lang/Enum;-><init>(Ljava/lang/String;I)V

    return-void
.end method

.method public static valueOf(Ljava/lang/String;)Lcom/helpshift/common/platform/Device$PermissionType;
    .locals 1

    .line 71
    const-class v0, Lcom/helpshift/common/platform/Device$PermissionType;

    invoke-static {v0, p0}, Ljava/lang/Enum;->valueOf(Ljava/lang/Class;Ljava/lang/String;)Ljava/lang/Enum;

    move-result-object p0

    check-cast p0, Lcom/helpshift/common/platform/Device$PermissionType;

    return-object p0
.end method

.method public static values()[Lcom/helpshift/common/platform/Device$PermissionType;
    .locals 1

    .line 71
    sget-object v0, Lcom/helpshift/common/platform/Device$PermissionType;->$VALUES:[Lcom/helpshift/common/platform/Device$PermissionType;

    invoke-virtual {v0}, [Lcom/helpshift/common/platform/Device$PermissionType;->clone()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, [Lcom/helpshift/common/platform/Device$PermissionType;

    return-object v0
.end method
