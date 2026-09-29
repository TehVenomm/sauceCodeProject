.class public final enum Lcom/helpshift/analytics/AnalyticsEventType;
.super Ljava/lang/Enum;
.source "AnalyticsEventType.java"


# annotations
.annotation system Ldalvik/annotation/Signature;
    value = {
        "Ljava/lang/Enum<",
        "Lcom/helpshift/analytics/AnalyticsEventType;",
        ">;"
    }
.end annotation


# static fields
.field private static final synthetic $VALUES:[Lcom/helpshift/analytics/AnalyticsEventType;

.field public static final enum ADMIN_MESSAGE_DEEPLINK_CLICKED:Lcom/helpshift/analytics/AnalyticsEventType;

.field public static final enum APP_START:Lcom/helpshift/analytics/AnalyticsEventType;

.field public static final enum BROWSED_FAQ_LIST:Lcom/helpshift/analytics/AnalyticsEventType;

.field public static final enum CANCEL_CSAT_RATING:Lcom/helpshift/analytics/AnalyticsEventType;

.field public static final enum CONVERSATION_POSTED:Lcom/helpshift/analytics/AnalyticsEventType;

.field public static final enum DYNAMIC_FORM_CLOSE:Lcom/helpshift/analytics/AnalyticsEventType;

.field public static final enum DYNAMIC_FORM_OPEN:Lcom/helpshift/analytics/AnalyticsEventType;

.field public static final enum LIBRARY_OPENED:Lcom/helpshift/analytics/AnalyticsEventType;

.field public static final enum LIBRARY_OPENED_DECOMP:Lcom/helpshift/analytics/AnalyticsEventType;

.field public static final enum LIBRARY_QUIT:Lcom/helpshift/analytics/AnalyticsEventType;

.field public static final enum LINK_VIA_FAQ:Lcom/helpshift/analytics/AnalyticsEventType;

.field public static final enum MARKED_HELPFUL:Lcom/helpshift/analytics/AnalyticsEventType;

.field public static final enum MARKED_UNHELPFUL:Lcom/helpshift/analytics/AnalyticsEventType;

.field public static final enum MESSAGE_ADDED:Lcom/helpshift/analytics/AnalyticsEventType;

.field public static final enum OPEN_INBOX:Lcom/helpshift/analytics/AnalyticsEventType;

.field public static final enum OPEN_ISSUE:Lcom/helpshift/analytics/AnalyticsEventType;

.field public static final enum PERFORMED_SEARCH:Lcom/helpshift/analytics/AnalyticsEventType;

.field public static final enum READ_FAQ:Lcom/helpshift/analytics/AnalyticsEventType;

.field public static final enum REPORTED_ISSUE:Lcom/helpshift/analytics/AnalyticsEventType;

.field public static final enum RESOLUTION_ACCEPTED:Lcom/helpshift/analytics/AnalyticsEventType;

.field public static final enum RESOLUTION_REJECTED:Lcom/helpshift/analytics/AnalyticsEventType;

.field public static final enum REVIEWED_APP:Lcom/helpshift/analytics/AnalyticsEventType;

.field public static final enum START_CSAT_RATING:Lcom/helpshift/analytics/AnalyticsEventType;

.field public static final enum SUPPORT_LAUNCH:Lcom/helpshift/analytics/AnalyticsEventType;

.field public static final enum TICKET_AVOIDANCE_FAILED:Lcom/helpshift/analytics/AnalyticsEventType;

.field public static final enum TICKET_AVOIDED:Lcom/helpshift/analytics/AnalyticsEventType;


# instance fields
.field public final key:Ljava/lang/String;


# direct methods
.method static constructor <clinit>()V
    .locals 16

    .line 8
    new-instance v0, Lcom/helpshift/analytics/AnalyticsEventType;

    const-string v1, "APP_START"

    const-string v2, "a"

    const/4 v3, 0x0

    invoke-direct {v0, v1, v3, v2}, Lcom/helpshift/analytics/AnalyticsEventType;-><init>(Ljava/lang/String;ILjava/lang/String;)V

    sput-object v0, Lcom/helpshift/analytics/AnalyticsEventType;->APP_START:Lcom/helpshift/analytics/AnalyticsEventType;

    .line 9
    new-instance v0, Lcom/helpshift/analytics/AnalyticsEventType;

    const-string v1, "LIBRARY_OPENED"

    const-string v2, "o"

    const/4 v4, 0x1

    invoke-direct {v0, v1, v4, v2}, Lcom/helpshift/analytics/AnalyticsEventType;-><init>(Ljava/lang/String;ILjava/lang/String;)V

    sput-object v0, Lcom/helpshift/analytics/AnalyticsEventType;->LIBRARY_OPENED:Lcom/helpshift/analytics/AnalyticsEventType;

    .line 10
    new-instance v0, Lcom/helpshift/analytics/AnalyticsEventType;

    const-string v1, "LIBRARY_OPENED_DECOMP"

    const-string v2, "d"

    const/4 v5, 0x2

    invoke-direct {v0, v1, v5, v2}, Lcom/helpshift/analytics/AnalyticsEventType;-><init>(Ljava/lang/String;ILjava/lang/String;)V

    sput-object v0, Lcom/helpshift/analytics/AnalyticsEventType;->LIBRARY_OPENED_DECOMP:Lcom/helpshift/analytics/AnalyticsEventType;

    .line 11
    new-instance v0, Lcom/helpshift/analytics/AnalyticsEventType;

    const-string v1, "SUPPORT_LAUNCH"

    const-string v2, "l"

    const/4 v6, 0x3

    invoke-direct {v0, v1, v6, v2}, Lcom/helpshift/analytics/AnalyticsEventType;-><init>(Ljava/lang/String;ILjava/lang/String;)V

    sput-object v0, Lcom/helpshift/analytics/AnalyticsEventType;->SUPPORT_LAUNCH:Lcom/helpshift/analytics/AnalyticsEventType;

    .line 12
    new-instance v0, Lcom/helpshift/analytics/AnalyticsEventType;

    const-string v1, "PERFORMED_SEARCH"

    const-string v2, "s"

    const/4 v7, 0x4

    invoke-direct {v0, v1, v7, v2}, Lcom/helpshift/analytics/AnalyticsEventType;-><init>(Ljava/lang/String;ILjava/lang/String;)V

    sput-object v0, Lcom/helpshift/analytics/AnalyticsEventType;->PERFORMED_SEARCH:Lcom/helpshift/analytics/AnalyticsEventType;

    .line 13
    new-instance v0, Lcom/helpshift/analytics/AnalyticsEventType;

    const-string v1, "BROWSED_FAQ_LIST"

    const-string v2, "b"

    const/4 v8, 0x5

    invoke-direct {v0, v1, v8, v2}, Lcom/helpshift/analytics/AnalyticsEventType;-><init>(Ljava/lang/String;ILjava/lang/String;)V

    sput-object v0, Lcom/helpshift/analytics/AnalyticsEventType;->BROWSED_FAQ_LIST:Lcom/helpshift/analytics/AnalyticsEventType;

    .line 14
    new-instance v0, Lcom/helpshift/analytics/AnalyticsEventType;

    const-string v1, "READ_FAQ"

    const-string v2, "f"

    const/4 v9, 0x6

    invoke-direct {v0, v1, v9, v2}, Lcom/helpshift/analytics/AnalyticsEventType;-><init>(Ljava/lang/String;ILjava/lang/String;)V

    sput-object v0, Lcom/helpshift/analytics/AnalyticsEventType;->READ_FAQ:Lcom/helpshift/analytics/AnalyticsEventType;

    .line 15
    new-instance v0, Lcom/helpshift/analytics/AnalyticsEventType;

    const-string v1, "MARKED_HELPFUL"

    const-string v2, "h"

    const/4 v10, 0x7

    invoke-direct {v0, v1, v10, v2}, Lcom/helpshift/analytics/AnalyticsEventType;-><init>(Ljava/lang/String;ILjava/lang/String;)V

    sput-object v0, Lcom/helpshift/analytics/AnalyticsEventType;->MARKED_HELPFUL:Lcom/helpshift/analytics/AnalyticsEventType;

    .line 16
    new-instance v0, Lcom/helpshift/analytics/AnalyticsEventType;

    const-string v1, "MARKED_UNHELPFUL"

    const-string v2, "u"

    const/16 v11, 0x8

    invoke-direct {v0, v1, v11, v2}, Lcom/helpshift/analytics/AnalyticsEventType;-><init>(Ljava/lang/String;ILjava/lang/String;)V

    sput-object v0, Lcom/helpshift/analytics/AnalyticsEventType;->MARKED_UNHELPFUL:Lcom/helpshift/analytics/AnalyticsEventType;

    .line 17
    new-instance v0, Lcom/helpshift/analytics/AnalyticsEventType;

    const-string v1, "REPORTED_ISSUE"

    const-string v2, "i"

    const/16 v12, 0x9

    invoke-direct {v0, v1, v12, v2}, Lcom/helpshift/analytics/AnalyticsEventType;-><init>(Ljava/lang/String;ILjava/lang/String;)V

    sput-object v0, Lcom/helpshift/analytics/AnalyticsEventType;->REPORTED_ISSUE:Lcom/helpshift/analytics/AnalyticsEventType;

    .line 18
    new-instance v0, Lcom/helpshift/analytics/AnalyticsEventType;

    const-string v1, "CONVERSATION_POSTED"

    const-string v2, "p"

    const/16 v13, 0xa

    invoke-direct {v0, v1, v13, v2}, Lcom/helpshift/analytics/AnalyticsEventType;-><init>(Ljava/lang/String;ILjava/lang/String;)V

    sput-object v0, Lcom/helpshift/analytics/AnalyticsEventType;->CONVERSATION_POSTED:Lcom/helpshift/analytics/AnalyticsEventType;

    .line 19
    new-instance v0, Lcom/helpshift/analytics/AnalyticsEventType;

    const-string v1, "REVIEWED_APP"

    const-string v2, "r"

    const/16 v14, 0xb

    invoke-direct {v0, v1, v14, v2}, Lcom/helpshift/analytics/AnalyticsEventType;-><init>(Ljava/lang/String;ILjava/lang/String;)V

    sput-object v0, Lcom/helpshift/analytics/AnalyticsEventType;->REVIEWED_APP:Lcom/helpshift/analytics/AnalyticsEventType;

    .line 20
    new-instance v0, Lcom/helpshift/analytics/AnalyticsEventType;

    const-string v1, "OPEN_ISSUE"

    const-string v2, "c"

    const/16 v15, 0xc

    invoke-direct {v0, v1, v15, v2}, Lcom/helpshift/analytics/AnalyticsEventType;-><init>(Ljava/lang/String;ILjava/lang/String;)V

    sput-object v0, Lcom/helpshift/analytics/AnalyticsEventType;->OPEN_ISSUE:Lcom/helpshift/analytics/AnalyticsEventType;

    .line 21
    new-instance v0, Lcom/helpshift/analytics/AnalyticsEventType;

    const-string v1, "OPEN_INBOX"

    const-string v2, "x"

    const/16 v15, 0xd

    invoke-direct {v0, v1, v15, v2}, Lcom/helpshift/analytics/AnalyticsEventType;-><init>(Ljava/lang/String;ILjava/lang/String;)V

    sput-object v0, Lcom/helpshift/analytics/AnalyticsEventType;->OPEN_INBOX:Lcom/helpshift/analytics/AnalyticsEventType;

    .line 22
    new-instance v0, Lcom/helpshift/analytics/AnalyticsEventType;

    const-string v1, "LIBRARY_QUIT"

    const-string v2, "q"

    const/16 v15, 0xe

    invoke-direct {v0, v1, v15, v2}, Lcom/helpshift/analytics/AnalyticsEventType;-><init>(Ljava/lang/String;ILjava/lang/String;)V

    sput-object v0, Lcom/helpshift/analytics/AnalyticsEventType;->LIBRARY_QUIT:Lcom/helpshift/analytics/AnalyticsEventType;

    .line 23
    new-instance v0, Lcom/helpshift/analytics/AnalyticsEventType;

    const-string v1, "MESSAGE_ADDED"

    const-string v2, "m"

    const/16 v15, 0xf

    invoke-direct {v0, v1, v15, v2}, Lcom/helpshift/analytics/AnalyticsEventType;-><init>(Ljava/lang/String;ILjava/lang/String;)V

    sput-object v0, Lcom/helpshift/analytics/AnalyticsEventType;->MESSAGE_ADDED:Lcom/helpshift/analytics/AnalyticsEventType;

    .line 24
    new-instance v0, Lcom/helpshift/analytics/AnalyticsEventType;

    const-string v1, "RESOLUTION_ACCEPTED"

    const-string v2, "y"

    const/16 v15, 0x10

    invoke-direct {v0, v1, v15, v2}, Lcom/helpshift/analytics/AnalyticsEventType;-><init>(Ljava/lang/String;ILjava/lang/String;)V

    sput-object v0, Lcom/helpshift/analytics/AnalyticsEventType;->RESOLUTION_ACCEPTED:Lcom/helpshift/analytics/AnalyticsEventType;

    .line 25
    new-instance v0, Lcom/helpshift/analytics/AnalyticsEventType;

    const-string v1, "RESOLUTION_REJECTED"

    const-string v2, "n"

    const/16 v15, 0x11

    invoke-direct {v0, v1, v15, v2}, Lcom/helpshift/analytics/AnalyticsEventType;-><init>(Ljava/lang/String;ILjava/lang/String;)V

    sput-object v0, Lcom/helpshift/analytics/AnalyticsEventType;->RESOLUTION_REJECTED:Lcom/helpshift/analytics/AnalyticsEventType;

    .line 26
    new-instance v0, Lcom/helpshift/analytics/AnalyticsEventType;

    const-string v1, "START_CSAT_RATING"

    const-string v2, "sr"

    const/16 v15, 0x12

    invoke-direct {v0, v1, v15, v2}, Lcom/helpshift/analytics/AnalyticsEventType;-><init>(Ljava/lang/String;ILjava/lang/String;)V

    sput-object v0, Lcom/helpshift/analytics/AnalyticsEventType;->START_CSAT_RATING:Lcom/helpshift/analytics/AnalyticsEventType;

    .line 27
    new-instance v0, Lcom/helpshift/analytics/AnalyticsEventType;

    const-string v1, "CANCEL_CSAT_RATING"

    const-string v2, "cr"

    const/16 v15, 0x13

    invoke-direct {v0, v1, v15, v2}, Lcom/helpshift/analytics/AnalyticsEventType;-><init>(Ljava/lang/String;ILjava/lang/String;)V

    sput-object v0, Lcom/helpshift/analytics/AnalyticsEventType;->CANCEL_CSAT_RATING:Lcom/helpshift/analytics/AnalyticsEventType;

    .line 28
    new-instance v0, Lcom/helpshift/analytics/AnalyticsEventType;

    const-string v1, "LINK_VIA_FAQ"

    const-string v2, "fl"

    const/16 v15, 0x14

    invoke-direct {v0, v1, v15, v2}, Lcom/helpshift/analytics/AnalyticsEventType;-><init>(Ljava/lang/String;ILjava/lang/String;)V

    sput-object v0, Lcom/helpshift/analytics/AnalyticsEventType;->LINK_VIA_FAQ:Lcom/helpshift/analytics/AnalyticsEventType;

    .line 29
    new-instance v0, Lcom/helpshift/analytics/AnalyticsEventType;

    const-string v1, "TICKET_AVOIDED"

    const-string v2, "ta"

    const/16 v15, 0x15

    invoke-direct {v0, v1, v15, v2}, Lcom/helpshift/analytics/AnalyticsEventType;-><init>(Ljava/lang/String;ILjava/lang/String;)V

    sput-object v0, Lcom/helpshift/analytics/AnalyticsEventType;->TICKET_AVOIDED:Lcom/helpshift/analytics/AnalyticsEventType;

    .line 30
    new-instance v0, Lcom/helpshift/analytics/AnalyticsEventType;

    const-string v1, "TICKET_AVOIDANCE_FAILED"

    const-string v2, "taf"

    const/16 v15, 0x16

    invoke-direct {v0, v1, v15, v2}, Lcom/helpshift/analytics/AnalyticsEventType;-><init>(Ljava/lang/String;ILjava/lang/String;)V

    sput-object v0, Lcom/helpshift/analytics/AnalyticsEventType;->TICKET_AVOIDANCE_FAILED:Lcom/helpshift/analytics/AnalyticsEventType;

    .line 31
    new-instance v0, Lcom/helpshift/analytics/AnalyticsEventType;

    const-string v1, "DYNAMIC_FORM_OPEN"

    const-string v2, "dfo"

    const/16 v15, 0x17

    invoke-direct {v0, v1, v15, v2}, Lcom/helpshift/analytics/AnalyticsEventType;-><init>(Ljava/lang/String;ILjava/lang/String;)V

    sput-object v0, Lcom/helpshift/analytics/AnalyticsEventType;->DYNAMIC_FORM_OPEN:Lcom/helpshift/analytics/AnalyticsEventType;

    .line 32
    new-instance v0, Lcom/helpshift/analytics/AnalyticsEventType;

    const-string v1, "ADMIN_MESSAGE_DEEPLINK_CLICKED"

    const-string v2, "ml"

    const/16 v15, 0x18

    invoke-direct {v0, v1, v15, v2}, Lcom/helpshift/analytics/AnalyticsEventType;-><init>(Ljava/lang/String;ILjava/lang/String;)V

    sput-object v0, Lcom/helpshift/analytics/AnalyticsEventType;->ADMIN_MESSAGE_DEEPLINK_CLICKED:Lcom/helpshift/analytics/AnalyticsEventType;

    .line 33
    new-instance v0, Lcom/helpshift/analytics/AnalyticsEventType;

    const-string v1, "DYNAMIC_FORM_CLOSE"

    const-string v2, "dfc"

    const/16 v15, 0x19

    invoke-direct {v0, v1, v15, v2}, Lcom/helpshift/analytics/AnalyticsEventType;-><init>(Ljava/lang/String;ILjava/lang/String;)V

    sput-object v0, Lcom/helpshift/analytics/AnalyticsEventType;->DYNAMIC_FORM_CLOSE:Lcom/helpshift/analytics/AnalyticsEventType;

    const/16 v0, 0x1a

    .line 6
    new-array v0, v0, [Lcom/helpshift/analytics/AnalyticsEventType;

    sget-object v1, Lcom/helpshift/analytics/AnalyticsEventType;->APP_START:Lcom/helpshift/analytics/AnalyticsEventType;

    aput-object v1, v0, v3

    sget-object v1, Lcom/helpshift/analytics/AnalyticsEventType;->LIBRARY_OPENED:Lcom/helpshift/analytics/AnalyticsEventType;

    aput-object v1, v0, v4

    sget-object v1, Lcom/helpshift/analytics/AnalyticsEventType;->LIBRARY_OPENED_DECOMP:Lcom/helpshift/analytics/AnalyticsEventType;

    aput-object v1, v0, v5

    sget-object v1, Lcom/helpshift/analytics/AnalyticsEventType;->SUPPORT_LAUNCH:Lcom/helpshift/analytics/AnalyticsEventType;

    aput-object v1, v0, v6

    sget-object v1, Lcom/helpshift/analytics/AnalyticsEventType;->PERFORMED_SEARCH:Lcom/helpshift/analytics/AnalyticsEventType;

    aput-object v1, v0, v7

    sget-object v1, Lcom/helpshift/analytics/AnalyticsEventType;->BROWSED_FAQ_LIST:Lcom/helpshift/analytics/AnalyticsEventType;

    aput-object v1, v0, v8

    sget-object v1, Lcom/helpshift/analytics/AnalyticsEventType;->READ_FAQ:Lcom/helpshift/analytics/AnalyticsEventType;

    aput-object v1, v0, v9

    sget-object v1, Lcom/helpshift/analytics/AnalyticsEventType;->MARKED_HELPFUL:Lcom/helpshift/analytics/AnalyticsEventType;

    aput-object v1, v0, v10

    sget-object v1, Lcom/helpshift/analytics/AnalyticsEventType;->MARKED_UNHELPFUL:Lcom/helpshift/analytics/AnalyticsEventType;

    aput-object v1, v0, v11

    sget-object v1, Lcom/helpshift/analytics/AnalyticsEventType;->REPORTED_ISSUE:Lcom/helpshift/analytics/AnalyticsEventType;

    aput-object v1, v0, v12

    sget-object v1, Lcom/helpshift/analytics/AnalyticsEventType;->CONVERSATION_POSTED:Lcom/helpshift/analytics/AnalyticsEventType;

    aput-object v1, v0, v13

    sget-object v1, Lcom/helpshift/analytics/AnalyticsEventType;->REVIEWED_APP:Lcom/helpshift/analytics/AnalyticsEventType;

    aput-object v1, v0, v14

    sget-object v1, Lcom/helpshift/analytics/AnalyticsEventType;->OPEN_ISSUE:Lcom/helpshift/analytics/AnalyticsEventType;

    const/16 v2, 0xc

    aput-object v1, v0, v2

    sget-object v1, Lcom/helpshift/analytics/AnalyticsEventType;->OPEN_INBOX:Lcom/helpshift/analytics/AnalyticsEventType;

    const/16 v2, 0xd

    aput-object v1, v0, v2

    sget-object v1, Lcom/helpshift/analytics/AnalyticsEventType;->LIBRARY_QUIT:Lcom/helpshift/analytics/AnalyticsEventType;

    const/16 v2, 0xe

    aput-object v1, v0, v2

    sget-object v1, Lcom/helpshift/analytics/AnalyticsEventType;->MESSAGE_ADDED:Lcom/helpshift/analytics/AnalyticsEventType;

    const/16 v2, 0xf

    aput-object v1, v0, v2

    sget-object v1, Lcom/helpshift/analytics/AnalyticsEventType;->RESOLUTION_ACCEPTED:Lcom/helpshift/analytics/AnalyticsEventType;

    const/16 v2, 0x10

    aput-object v1, v0, v2

    sget-object v1, Lcom/helpshift/analytics/AnalyticsEventType;->RESOLUTION_REJECTED:Lcom/helpshift/analytics/AnalyticsEventType;

    const/16 v2, 0x11

    aput-object v1, v0, v2

    sget-object v1, Lcom/helpshift/analytics/AnalyticsEventType;->START_CSAT_RATING:Lcom/helpshift/analytics/AnalyticsEventType;

    const/16 v2, 0x12

    aput-object v1, v0, v2

    sget-object v1, Lcom/helpshift/analytics/AnalyticsEventType;->CANCEL_CSAT_RATING:Lcom/helpshift/analytics/AnalyticsEventType;

    const/16 v2, 0x13

    aput-object v1, v0, v2

    sget-object v1, Lcom/helpshift/analytics/AnalyticsEventType;->LINK_VIA_FAQ:Lcom/helpshift/analytics/AnalyticsEventType;

    const/16 v2, 0x14

    aput-object v1, v0, v2

    sget-object v1, Lcom/helpshift/analytics/AnalyticsEventType;->TICKET_AVOIDED:Lcom/helpshift/analytics/AnalyticsEventType;

    const/16 v2, 0x15

    aput-object v1, v0, v2

    sget-object v1, Lcom/helpshift/analytics/AnalyticsEventType;->TICKET_AVOIDANCE_FAILED:Lcom/helpshift/analytics/AnalyticsEventType;

    const/16 v2, 0x16

    aput-object v1, v0, v2

    sget-object v1, Lcom/helpshift/analytics/AnalyticsEventType;->DYNAMIC_FORM_OPEN:Lcom/helpshift/analytics/AnalyticsEventType;

    const/16 v2, 0x17

    aput-object v1, v0, v2

    sget-object v1, Lcom/helpshift/analytics/AnalyticsEventType;->ADMIN_MESSAGE_DEEPLINK_CLICKED:Lcom/helpshift/analytics/AnalyticsEventType;

    const/16 v2, 0x18

    aput-object v1, v0, v2

    sget-object v1, Lcom/helpshift/analytics/AnalyticsEventType;->DYNAMIC_FORM_CLOSE:Lcom/helpshift/analytics/AnalyticsEventType;

    const/16 v2, 0x19

    aput-object v1, v0, v2

    sput-object v0, Lcom/helpshift/analytics/AnalyticsEventType;->$VALUES:[Lcom/helpshift/analytics/AnalyticsEventType;

    return-void
.end method

.method private constructor <init>(Ljava/lang/String;ILjava/lang/String;)V
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/String;",
            ")V"
        }
    .end annotation

    .line 37
    invoke-direct {p0, p1, p2}, Ljava/lang/Enum;-><init>(Ljava/lang/String;I)V

    .line 38
    iput-object p3, p0, Lcom/helpshift/analytics/AnalyticsEventType;->key:Ljava/lang/String;

    return-void
.end method

.method public static valueOf(Ljava/lang/String;)Lcom/helpshift/analytics/AnalyticsEventType;
    .locals 1

    .line 6
    const-class v0, Lcom/helpshift/analytics/AnalyticsEventType;

    invoke-static {v0, p0}, Ljava/lang/Enum;->valueOf(Ljava/lang/Class;Ljava/lang/String;)Ljava/lang/Enum;

    move-result-object p0

    check-cast p0, Lcom/helpshift/analytics/AnalyticsEventType;

    return-object p0
.end method

.method public static values()[Lcom/helpshift/analytics/AnalyticsEventType;
    .locals 1

    .line 6
    sget-object v0, Lcom/helpshift/analytics/AnalyticsEventType;->$VALUES:[Lcom/helpshift/analytics/AnalyticsEventType;

    invoke-virtual {v0}, [Lcom/helpshift/analytics/AnalyticsEventType;->clone()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, [Lcom/helpshift/analytics/AnalyticsEventType;

    return-object v0
.end method
