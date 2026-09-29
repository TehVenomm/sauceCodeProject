.class public final enum Lnet/gogame/gowrap/ui/dialog/CustomDialog$Type;
.super Ljava/lang/Enum;
.source "CustomDialog.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lnet/gogame/gowrap/ui/dialog/CustomDialog;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x4019
    name = "Type"
.end annotation

.annotation system Ldalvik/annotation/Signature;
    value = {
        "Ljava/lang/Enum<",
        "Lnet/gogame/gowrap/ui/dialog/CustomDialog$Type;",
        ">;"
    }
.end annotation


# static fields
.field private static final synthetic $VALUES:[Lnet/gogame/gowrap/ui/dialog/CustomDialog$Type;

.field public static final enum ALERT:Lnet/gogame/gowrap/ui/dialog/CustomDialog$Type;

.field public static final enum INFO:Lnet/gogame/gowrap/ui/dialog/CustomDialog$Type;

.field public static final enum PROGRESS:Lnet/gogame/gowrap/ui/dialog/CustomDialog$Type;


# direct methods
.method static constructor <clinit>()V
    .locals 5

    .line 171
    new-instance v0, Lnet/gogame/gowrap/ui/dialog/CustomDialog$Type;

    const-string v1, "INFO"

    const/4 v2, 0x0

    invoke-direct {v0, v1, v2}, Lnet/gogame/gowrap/ui/dialog/CustomDialog$Type;-><init>(Ljava/lang/String;I)V

    sput-object v0, Lnet/gogame/gowrap/ui/dialog/CustomDialog$Type;->INFO:Lnet/gogame/gowrap/ui/dialog/CustomDialog$Type;

    .line 172
    new-instance v0, Lnet/gogame/gowrap/ui/dialog/CustomDialog$Type;

    const-string v1, "ALERT"

    const/4 v3, 0x1

    invoke-direct {v0, v1, v3}, Lnet/gogame/gowrap/ui/dialog/CustomDialog$Type;-><init>(Ljava/lang/String;I)V

    sput-object v0, Lnet/gogame/gowrap/ui/dialog/CustomDialog$Type;->ALERT:Lnet/gogame/gowrap/ui/dialog/CustomDialog$Type;

    .line 173
    new-instance v0, Lnet/gogame/gowrap/ui/dialog/CustomDialog$Type;

    const-string v1, "PROGRESS"

    const/4 v4, 0x2

    invoke-direct {v0, v1, v4}, Lnet/gogame/gowrap/ui/dialog/CustomDialog$Type;-><init>(Ljava/lang/String;I)V

    sput-object v0, Lnet/gogame/gowrap/ui/dialog/CustomDialog$Type;->PROGRESS:Lnet/gogame/gowrap/ui/dialog/CustomDialog$Type;

    const/4 v0, 0x3

    .line 169
    new-array v0, v0, [Lnet/gogame/gowrap/ui/dialog/CustomDialog$Type;

    sget-object v1, Lnet/gogame/gowrap/ui/dialog/CustomDialog$Type;->INFO:Lnet/gogame/gowrap/ui/dialog/CustomDialog$Type;

    aput-object v1, v0, v2

    sget-object v1, Lnet/gogame/gowrap/ui/dialog/CustomDialog$Type;->ALERT:Lnet/gogame/gowrap/ui/dialog/CustomDialog$Type;

    aput-object v1, v0, v3

    sget-object v1, Lnet/gogame/gowrap/ui/dialog/CustomDialog$Type;->PROGRESS:Lnet/gogame/gowrap/ui/dialog/CustomDialog$Type;

    aput-object v1, v0, v4

    sput-object v0, Lnet/gogame/gowrap/ui/dialog/CustomDialog$Type;->$VALUES:[Lnet/gogame/gowrap/ui/dialog/CustomDialog$Type;

    return-void
.end method

.method private constructor <init>(Ljava/lang/String;I)V
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()V"
        }
    .end annotation

    .line 169
    invoke-direct {p0, p1, p2}, Ljava/lang/Enum;-><init>(Ljava/lang/String;I)V

    return-void
.end method

.method public static valueOf(Ljava/lang/String;)Lnet/gogame/gowrap/ui/dialog/CustomDialog$Type;
    .locals 1

    .line 169
    const-class v0, Lnet/gogame/gowrap/ui/dialog/CustomDialog$Type;

    invoke-static {v0, p0}, Ljava/lang/Enum;->valueOf(Ljava/lang/Class;Ljava/lang/String;)Ljava/lang/Enum;

    move-result-object p0

    check-cast p0, Lnet/gogame/gowrap/ui/dialog/CustomDialog$Type;

    return-object p0
.end method

.method public static values()[Lnet/gogame/gowrap/ui/dialog/CustomDialog$Type;
    .locals 1

    .line 169
    sget-object v0, Lnet/gogame/gowrap/ui/dialog/CustomDialog$Type;->$VALUES:[Lnet/gogame/gowrap/ui/dialog/CustomDialog$Type;

    invoke-virtual {v0}, [Lnet/gogame/gowrap/ui/dialog/CustomDialog$Type;->clone()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, [Lnet/gogame/gowrap/ui/dialog/CustomDialog$Type;

    return-object v0
.end method
